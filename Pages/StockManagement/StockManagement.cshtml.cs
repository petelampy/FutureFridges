using FutureFridges.Business.StockManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutureFridges.Pages.StockManagement
{
    public class StockManagementModel : PageModel
    {
        private readonly ProductController __ProductController;
        private readonly StockItemController __StockItemController;

        public StockManagementModel ()
        {
            __StockItemController = new StockItemController();
            __ProductController = new ProductController();
        }

        public void OnGet ()
        {
            StockItems = __StockItemController.GetAll();
            Products = __ProductController.GetAll();
        }

        public void OnPostSelectProduct (Guid uid)
        {
            StockItems = __StockItemController.GetAll();
            Products = __ProductController.GetAll();
            SelectedProduct = __ProductController.GetProduct(uid);
        }

        public async Task<IActionResult> OnGetTakeProduct (Guid uid)
        {
            StockItem _CurrentItem = __StockItemController.GetStockItem(uid);
            __StockItemController.DeleteStockItem(uid);

            StockItems = __StockItemController.GetAll();
            Products = __ProductController.GetAll();

            int _RemainingQuantity = StockItems.Where(stockItem => stockItem.Product_UID == _CurrentItem.Product_UID).Count();
            if (_RemainingQuantity > 0)
            {
                SelectedProduct = __ProductController.GetProduct(_CurrentItem.Product_UID);
                return Page();
            }
            else
            {
                return RedirectToPage("StockManagement");
            }
        }

        public List<Product> Products { get; set; }
        public Product? SelectedProduct { get; set; } = new Product();
        public List<StockItem> StockItems { get; set; }
        public string ProductImagePath { get; set; } = "../Images/Products/";
    }
}
