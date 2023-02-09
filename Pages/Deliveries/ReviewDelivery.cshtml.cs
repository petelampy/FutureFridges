using FutureFridges.Business.AuditLog;
using FutureFridges.Business.Enums;
using FutureFridges.Business.OrderManagement;
using FutureFridges.Business.StockManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace FutureFridges.Pages.Deliveries
{
    public class ReviewDeliveryModel : PageModel
    {
        private const string LOG_RECEIVE_FORMAT = "Delivery received containing: {0}";

        private readonly IOrderController __OrderController;
        private readonly IProductController __ProductController;
        private readonly IStockItemController __StockItemController;
        private readonly IAuditLogController __AuditLogController;
        private readonly ISupplierController __SupplierController;

        public ReviewDeliveryModel ()
        {
            __OrderController = new OrderController();
            __StockItemController = new StockItemController();
            __ProductController = new ProductController();
            __AuditLogController = new AuditLogController();
            __SupplierController = new SupplierController();
        }

        public void OnGet ()
        {
            Order = __OrderController.GetOrderByPinCode(PinCode);
        }

        public IActionResult OnGetCompleteDelivery (Guid uid)
        {
            Order = __OrderController.GetOrder(uid);

            List<StockItem> _StockItems = new List<StockItem>();

            foreach (OrderItem _OrderItem in Order.OrderItems)
            {
                for (int _Counter = 0; _Counter < _OrderItem.Quantity; _Counter++)
                {
                    int _ShelfLife = __ProductController.GetProduct(_OrderItem.Product_UID).DaysShelfLife;

                    StockItem _StockItem = new StockItem
                    {
                        Product_UID = _OrderItem.Product_UID,
                        ExpiryDate = DateTime.Now.AddDays(_ShelfLife),
                    };

                    _StockItems.Add(_StockItem);
                }
            }

            List<Product> _Products = __ProductController.GetProducts(Order.OrderItems.Select(orderItem => orderItem.Product_UID).ToList());
            Supplier _Supplier = __SupplierController.Get(Order.Supplier_UID.Value);

            StringBuilder _StringBuilder = new StringBuilder();

            foreach (OrderItem _Item in Order.OrderItems)
            {
                string? _ProductName = _Products.Where(product => product.UID == _Item.Product_UID).SingleOrDefault().Name;

                _StringBuilder.Append(_Item.Quantity + " x " + _ProductName + ", ");
            }   

            __AuditLogController.Create(_Supplier.Name, string.Format(LOG_RECEIVE_FORMAT, _StringBuilder), LogType.DeliveryReceive);

            __StockItemController.CreateStockItem(_StockItems);

            __OrderController.DeleteOrder(uid);

            return RedirectToPage("../Index");
        }


        [BindProperty(SupportsGet = true)]
        public Order Order { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PinCode { get; set; }
    }
}
