using FutureFridges.Business.StockManagement;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutureFridges.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> __Logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            __Logger = logger;
        }

        public void OnGet()
        {
            Guid _ExampleStockItemGuid = Guid.Empty;

            StockItemController _StockItemController = new StockItemController();
            _StockItemController.GetStockItem(_ExampleStockItemGuid);
        }
    }
}