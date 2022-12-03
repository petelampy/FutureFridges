using FutureFridges.Business.StockManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutureFridges.Pages
{
    [BindProperties]
    public class ProductManagementModel : PageModel
    {
        public List<Product> Products { get; set; }

        public void OnGet()
        {
            ProductController _ProductController = new ProductController();
            Products = _ProductController.GetAll();
        }
    }
}
