using FutureFridges.Business.StockManagement;
using FutureFridges.Data;
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
            Guid _ExampleProductGuid = new Guid("c0c1847b-1007-4e1e-820e-86976226c158");

            ProductController _ProductController = new ProductController();
            Product _TestProduct = _ProductController.GetProduct(_ExampleProductGuid);
        }
    }
}