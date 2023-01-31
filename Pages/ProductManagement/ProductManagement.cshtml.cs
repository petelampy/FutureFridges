using FutureFridges.Business.AuditLog;
using FutureFridges.Business.Enums;
using FutureFridges.Business.OrderManagement;
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
        private const string LOG_ENTRY_DELETE = "{0} was deleted";

        private readonly IProductController __ProductController;
        private readonly IUserPermissionController __UserPermissionController;
        private readonly ISupplierController __SupplierController;
        private readonly IAuditLogController __AuditLogController;

        public ProductManagementModel ()
        {
            __ProductController = new ProductController();
            __UserPermissionController = new UserPermissionController();
            __SupplierController = new SupplierController();
            __AuditLogController = new AuditLogController();
        }

        public bool IsProductInUse (Guid uid)
        {
            return __ProductController.IsProductInUse(uid);
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
            if (__ProductController.IsProductInUse(uid))
            {
                ModelState.AddModelError("", "Product in use, can't delete!");
                return Page();
            }
            else
            {
                string? _ProductName = __ProductController.GetProduct(uid).Name;
                __AuditLogController.Create(User.Identity.Name, string.Format(LOG_ENTRY_DELETE, _ProductName), LogType.ProductDelete);

                __ProductController.DeleteProduct(uid);
                return RedirectToPage("ProductManagement");
            }
        }

        internal string GetSupplierName (Guid uid)
        {
            return __SupplierController.Get(uid).Name;
        }

        public List<Product> Products { get; set; }
    }
}
