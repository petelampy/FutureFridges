using FutureFridges.Business.OrderManagement;
using FutureFridges.Business.StockManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutureFridges.Pages.Deliveries
{
    public class ReviewDeliveryModel : PageModel
    {
        private readonly IOrderController __OrderController;
        private readonly IStockItemController __StockItemController;
        private readonly IProductController __ProductController;

        public ReviewDeliveryModel ()
        {
            __OrderController = new OrderController();
            __StockItemController = new StockItemController();
            __ProductController = new ProductController();
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

                    StockItem _StockItem = new StockItem()
                    {
                        Product_UID = _OrderItem.Product_UID,
                        ExpiryDate = DateTime.Now.AddDays(_ShelfLife),
                    };

                    _StockItems.Add(_StockItem);
                }
            }

            __StockItemController.CreateStockItem(_StockItems);

            return RedirectToPage("../Index");
        }


        [BindProperty(SupportsGet = true)]
        public Order Order { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PinCode { get; set; }
    }
}
