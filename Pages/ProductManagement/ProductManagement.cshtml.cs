using FutureFridges.Business.StockManagement;
using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FutureFridges.Pages
{
    [Authorize]
    [BindProperties]
    public class ProductManagementModel : PageModel
    {
        private const string ACCESS_ERROR_PAGE_PATH = "../Account/AccessError";

        private readonly ProductController __ProductController;
        private readonly UserPermissionController __UserPermissionController;

        public ProductManagementModel ()
        {
            __ProductController = new ProductController();
            __UserPermissionController = new UserPermissionController();
        }

        public IActionResult OnGet ()
        {
            string _CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserPermissions _CurrentUserPermissions = __UserPermissionController.GetPermissions(new Guid(_CurrentUserID));

            if (_CurrentUserPermissions.ManageProduct)
            {
                Products = __ProductController.GetAll();

                return Page();
            }
            else
            {
                return RedirectToPage(ACCESS_ERROR_PAGE_PATH);
            }

        }

        public async Task<IActionResult> OnGetDeleteProduct (Guid uid)
        {
            if(__ProductController.IsProductInUse(uid))
            {
                ModelState.AddModelError("", "Product in use, can't delete!");
                return Page();
            }
            else
            {
                __ProductController.DeleteProduct(uid);
                return RedirectToPage("ProductManagement");
            } 
        }

        public List<Product> Products { get; set; }
    }
}
