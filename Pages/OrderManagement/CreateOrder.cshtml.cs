using FutureFridges.Business.OrderManagement;
using FutureFridges.Business.StockManagement;
using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace FutureFridges.Pages.OrderManagement
{
    [Authorize]
    public class CreateOrderModel : PageModel
    {
        private const string ACCESS_ERROR_PAGE_PATH = "../Account/AccessError";

        private readonly IOrderController __OrderController;
        private readonly IProductController __ProductController;
        private readonly ISupplierController __SupplierController;
        private readonly IUserPermissionController __UserPermissionController;

        public CreateOrderModel ()
        {
            __OrderController = new OrderController();
            __ProductController = new ProductController();
            __UserPermissionController = new UserPermissionController();
            __SupplierController = new SupplierController();
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

            return RedirectToPage("../Index");
        }

        public IActionResult OnGetCreateOrder (Guid uid)
        {
            //__OrderController.CompleteOrder(uid);

            return RedirectToPage("../Index");
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

            if (Order.OrderItems.Select(orderItem => orderItem.Product_UID).Contains(_SelectedProduct_UID))
            {
                Order.OrderItems
                    .Where(orderItem => orderItem.Product_UID == _SelectedProduct_UID)
                    .ToList()
                    .ForEach(orderItem => orderItem.Quantity += SelectedProductQuantity);
            }
            else
            {
                OrderItem _AddedItem = new OrderItem();
                _AddedItem.Product_UID = _SelectedProduct_UID;
                _AddedItem.Quantity = SelectedProductQuantity;
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
        public string SelectedProduct { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SelectedProductQuantity { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid UID { get; set; }
    }
}
