using FutureFridges.Business.StockManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutureFridges.Pages.ProductManagement
{
    public class CreateEditProductModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid UID { get; set; }

        [BindProperty]
        public Product Product { get; set; }

        public void OnGet()
        {
            //MAYBE ADD AN ON GET FLAG TO DETERMINE IF IT'S A CREATE/EDIT RATHER THAN RELYING ON THE UID.
            //THIS WOULD ALSO ALLOW FOR A "VIEW" MODE WHERE ALL SAVE BUTTONS AND FIELDS ARE DISABLED JUST FOR VIEWING A PRODUCT

            if(UID != Guid.Empty)
            {
                ProductController _ProductController = new ProductController();
                Product = _ProductController.GetProduct(UID);
            }
            else
            {
                Product = new Product();
            }
        }

        public IActionResult OnPost ()
        {
            //MAKE THE PRODUCTCONTROLLER WORK AS A CLASS VARIABLE SO IT CAN BE USED WITHOUT REDECLARING?
            ProductController _ProductController = new ProductController();

            if(UID != Guid.Empty)
            {
                _ProductController.UpdateProduct(Product);
            }
            else
            {
                _ProductController.CreateProduct(Product);
            }

            return RedirectToPage("ProductManagement");
        }
    }
}
