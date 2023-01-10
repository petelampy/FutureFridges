using FutureFridges.Business.OrderManagement;
using FutureFridges.Business.StockManagement;
using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace FutureFridges.Pages.OrderManagement
{
    public class CreateOrderModel : PageModel
    {
        private readonly IOrderController __OrderController;
        private readonly IProductController __ProductController;

        public CreateOrderModel ()
        {
            __OrderController = new OrderController();
            __ProductController = new ProductController();
        }

        public void OnGet()
        {
            CreateProductSelector();

            //ADD A PERMISSION FOR CREATE ORDER PAGE TO PREVENT ACCESS IF NOT ALLOWED
            if (UID != Guid.Empty)
            {
                Order = __OrderController.GetOrder(UID);
            }
            else
            {
                Order = new Order();
                Order.OrderItems = new List<OrderItem>();
            }
        }

        private void CreateProductSelector()
        {
            List<Product> _Products = __ProductController.GetAll();

            ProductSelection = _Products.Select(product => 
                new SelectListItem
                {
                    Text = product.Name,
                    Value = product.Product_UID.ToString()
                }).ToList();
        }

        private void UpdateOrder()
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

        public IActionResult OnPostAddItem()
        {
            Order.OrderItems = __OrderController.GetOrderItems(Order.UID);
            Guid _SelectedProduct_UID = new Guid(SelectedProduct);

            if (Order.OrderItems.Select(orderItem => orderItem.Product_UID).Contains(_SelectedProduct_UID))
            {
                Order.OrderItems
                    .Where(orderItem => orderItem.Product_UID == _SelectedProduct_UID)
                    .ToList()
                    .ForEach(orderItem => orderItem.Quantity++);
            }
            else
            {
                OrderItem _AddedItem = new OrderItem();
                _AddedItem.Product_UID = _SelectedProduct_UID;
                _AddedItem.Quantity = 1;

                Order.OrderItems.Add(_AddedItem);
            }

            UpdateOrder();

            return RedirectToPage("CreateOrder", new { UID = Order.UID });
        }

        public IActionResult OnGetRemoveItem (Guid item_uid, Guid order_uid)
        {
            __OrderController.DeleteOrderItem(item_uid);

            return RedirectToPage("CreateOrder", new { UID = order_uid });
        }

        public IActionResult OnGetCreateOrder (Guid uid)
        {
           return RedirectToPage("../Index");
        }

        public IActionResult OnGetCancelOrder (Guid uid)
        {
            __OrderController.DeleteOrder(uid);

            return RedirectToPage("../Index");
        }

        [BindProperty(SupportsGet = true)]
        public Order Order { get; set; }
        [BindProperty(SupportsGet = true)]
        public Guid UID { get; set; }
        public List<SelectListItem> ProductSelection { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SelectedProduct { get; set; }
    }
}
