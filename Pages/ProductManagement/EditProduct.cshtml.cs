using FutureFridges.Business.StockManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutureFridges.Pages.ProductManagement
{
    public class EditProductModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid UID { get; set; }

        [BindProperty]
        public Product Product { get; set; }

        public void OnGet()
        {
            ProductController _ProductController = new ProductController();
            Product = _ProductController.GetProduct(UID);
        }

        public IActionResult OnPost ()
        {
            //MAKE THE PRODUCTCONTROLLER WORK AS A CLASS VARIABLE SO IT CAN BE USED WITHOUT REDECLARING?
            ProductController _ProductController = new ProductController();
            _ProductController.UpdateProduct(Product);
            return RedirectToPage("ProductManagement");
        }
    }
}
