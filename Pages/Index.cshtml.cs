using FutureFridges.Business.StockManagement;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutureFridges.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            int _ExampleStockItemID = 1;

            StockItemController _StockItemController = new StockItemController();
            _StockItemController.GetStockItem(_ExampleStockItemID);
        }
    }
}