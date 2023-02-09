using FutureFridges.Business.AuditLog;
using FutureFridges.Business.Enums;
using FutureFridges.Business.OrderManagement;
using FutureFridges.Business.StockManagement;
using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Text;

namespace FutureFridges.Pages.OrderManagement
{
    [Authorize]
    public class CreateOrderModel : PageModel
    {
        private const string ACCESS_ERROR_PAGE_PATH = "../Account/AccessError";
        private const string LOG_CREATE_FORMAT = "An order was created for: {0}";

        private readonly IOrderController __OrderController;
        private readonly IProductController __ProductController;
        private readonly ISupplierController __SupplierController;
        private readonly IUserPermissionController __UserPermissionController;
        private readonly IAuditLogController __AuditLogController;

        public CreateOrderModel ()
        {
            __OrderController = new OrderController();
            __ProductController = new ProductController();
            __UserPermissionController = new UserPermissionController();
            __SupplierController = new SupplierController();
            __AuditLogController = new AuditLogController();
        }

        private void CreateProductSelector ()
        {
            List<Product> _Products = __ProductController.GetAll();

            ProductSelection = _Products.Select(product =>
                new SelectListItem
                {
                    Text = product.Name,
                    Value = product.UID.ToString()
                }).ToList();
        }

        internal string GetProductSupplierName (Guid uid)
        {
            return __SupplierController.Get(uid).Name;
        }

        public IActionResult OnGet ()
        {
            string _CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserPermissions _CurrentUserPermissions = __UserPermissionController.GetPermissions(new Guid(_CurrentUserID));

            if (_CurrentUserPermissions.CreateOrder)
            {
                CreateProductSelector();
                SelectedProductQuantity = 1;

                if (UID != Guid.Empty)
                {
                    Order = __OrderController.GetOrder(UID);
                }
                else
                {
                    Order = new Order();
                    Order.OrderItems = new List<OrderItem>();
                }

                return Page();

            }
            else
            {
                return RedirectToPage(ACCESS_ERROR_PAGE_PATH);
            }
        }

        public IActionResult OnGetCancelOrder (Guid uid)
        {
            if (uid != Guid.Empty)
            {
                __OrderController.DeleteOrder(uid);
            }

            return RedirectToPage("OrderManagement");
        }

        private void ValidateOrderCreate(Guid uid)
        {
            if(uid == Guid.Empty || __OrderController.GetOrder(uid).OrderItems.Count < 1)
            {
                ModelState.AddModelError("", "Cannot create an order with 0 items");
            }

        }

        public IActionResult OnGetCreateOrder (Guid uid)
        {
            ValidateOrderCreate(uid);
            if(ModelState.ErrorCount > 0)
            {
                CreateProductSelector();
                SelectedProductQuantity = 1;

                if (UID != Guid.Empty)
                {
                    Order = __OrderController.GetOrder(UID);
                }
                else
                {
                    Order = new Order();
                    Order.OrderItems = new List<OrderItem>();
                }

                return Page();
            }

            List<OrderItem> _OrderItems = __OrderController.GetOrder(uid).OrderItems;
            List<Product> _Products = __ProductController.GetProducts(_OrderItems.Select(orderItem => orderItem.Product_UID).ToList());

            StringBuilder _StringBuilder = new StringBuilder();
          
            foreach (OrderItem _Item in _OrderItems)
            {
                string? _ProductName = _Products.Where(product => product.UID == _Item.Product_UID).SingleOrDefault().Name;

                _StringBuilder.Append(_Item.Quantity + " x " + _ProductName + ", ");
            }

            __AuditLogController.Create(User.Identity.Name, string.Format(LOG_CREATE_FORMAT, _StringBuilder), LogType.OrderCreate);

            __OrderController.CompleteOrder(uid);

            return RedirectToPage("OrderManagement");
        }

        public IActionResult OnGetRemoveItem (Guid item_uid, Guid order_uid)
        {
            __OrderController.DeleteOrderItem(item_uid);

            return RedirectToPage("CreateOrder", new { UID = order_uid });
        }

        public IActionResult OnPostAddItem ()
        {
            Order.OrderItems = __OrderController.GetOrderItems(Order.UID);
            Guid _SelectedProduct_UID = new Guid(SelectedProduct);

            if(SelectedProductQuantity.Value < 1)
            {
                ModelState.AddModelError("", "Cannot add less than 1 of a product");
                
                CreateProductSelector();
                SelectedProductQuantity = 1;

                if (UID != Guid.Empty)
                {
                    Order = __OrderController.GetOrder(UID);
                }
                else
                {
                    Order = new Order();
                    Order.OrderItems = new List<OrderItem>();
                }

                return Page();
            }

            if (Order.OrderItems.Select(orderItem => orderItem.Product_UID).Contains(_SelectedProduct_UID))
            {
                Order.OrderItems
                    .Where(orderItem => orderItem.Product_UID == _SelectedProduct_UID)
                    .ToList()
                    .ForEach(orderItem => orderItem.Quantity += SelectedProductQuantity.Value);
            }
            else
            {
                OrderItem _AddedItem = new OrderItem();
                _AddedItem.Product_UID = _SelectedProduct_UID;
                _AddedItem.Quantity = SelectedProductQuantity.Value;
                _AddedItem.Supplier_UID = __ProductController.GetProduct(_SelectedProduct_UID).Supplier_UID;

                Order.OrderItems.Add(_AddedItem);
            }

            UpdateOrder();

            return RedirectToPage("CreateOrder", new { UID = Order.UID });
        }

        private void UpdateOrder ()
        {
            if (Order.UID == Guid.Empty)
            {
                __OrderController.CreateOrder(Order);
            }

            foreach (OrderItem _OrderItem in Order.OrderItems)
            {
                if (_OrderItem.UID == Guid.Empty)
                {
                    _OrderItem.Order_UID = Order.UID;
                    __OrderController.CreateOrderItem(_OrderItem);
                }
                else
                {
                    __OrderController.UpdateOrderItem(_OrderItem);
                }

            }
        }

        [BindProperty(SupportsGet = true)]
        public Order Order { get; set; }

        public List<SelectListItem> ProductSelection { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SelectedProduct { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SelectedProductQuantity { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid UID { get; set; }
    }
}
