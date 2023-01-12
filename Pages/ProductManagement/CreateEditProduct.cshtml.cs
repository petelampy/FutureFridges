using FutureFridges.Business.StockManagement;
using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FutureFridges.Pages.ProductManagement
{
    [Authorize]
    public class CreateEditProductModel : PageModel
    {
        private const string ACCESS_ERROR_PAGE_PATH = "../Account/AccessError"; //MOVE PAGE PATHS INTO A GLOBAL RESX FILE??
        private const string PRODUCT_IMAGES_PATH = "wwwroot/Images/Products";

        private readonly IWebHostEnvironment __Environment;
        private readonly ProductController __ProductController;
        private readonly UserPermissionController __UserPermissionController;

        public CreateEditProductModel (IWebHostEnvironment environment)
        {
            __ProductController = new ProductController();
            __UserPermissionController = new UserPermissionController();
            __Environment = environment;
        }

        private async void CreateProductImageFile (IFormFile file)
        {
            string _FilePath = Path.Combine(__Environment.ContentRootPath, PRODUCT_IMAGES_PATH, file.FileName);
            using (FileStream _FileStream = new FileStream(_FilePath, FileMode.Create))
            {
                await file.CopyToAsync(_FileStream);
            }
        }

        public IActionResult OnGet ()
        {
            //MAYBE ADD AN ON GET FLAG TO DETERMINE IF IT'S A CREATE/EDIT RATHER THAN RELYING ON THE UID.
            //THIS WOULD ALSO ALLOW FOR A "VIEW" MODE WHERE ALL SAVE BUTTONS AND FIELDS ARE DISABLED JUST FOR VIEWING A PRODUCT

            string _CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserPermissions _CurrentUserPermissions = __UserPermissionController.GetPermissions(new Guid(_CurrentUserID));

            if (_CurrentUserPermissions.ManageProduct)
            {
                if (UID != Guid.Empty)
                {
                    Product = __ProductController.GetProduct(UID);
                }
                else
                {
                    Product = new Product();
                }

                return Page();
            }
            else
            {
                return RedirectToPage(ACCESS_ERROR_PAGE_PATH);
            }


        }

        public IActionResult OnPost ()
        {
            if (UID != Guid.Empty)
            {
                if (FileUpload != null)
                {
                    Product.ImageName = FileUpload.FileName;
                    CreateProductImageFile(FileUpload);
                }

                __ProductController.UpdateProduct(Product);
            }
            else
            {
                if (FileUpload != null)
                {
                    Product.ImageName = FileUpload.FileName;
                    CreateProductImageFile(FileUpload);
                }

                __ProductController.CreateProduct(Product);
            }

            return RedirectToPage("ProductManagement");
        }

        [BindProperty]
        public IFormFile FileUpload { get; set; }

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid UID { get; set; }
    }
}
