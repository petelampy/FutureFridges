using FutureFridges.Business.AuditLog;
using FutureFridges.Business.Enums;
using FutureFridges.Business.OrderManagement;
using FutureFridges.Business.StockManagement;
using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FutureFridges.Pages.SupplierManagement
{
    public class SupplierManagementModel : PageModel
    {
        private const string LOG_ENTRY_DELETE = "{0} was deleted";
        private const string ACCESS_ERROR_PAGE_PATH = "../Account/AccessError";

        private readonly IProductController __ProductController;
        private readonly ISupplierController __SupplierController;
        private readonly IAuditLogController __AuditLogController;
        private readonly IUserPermissionController __UserPermissionController;

        public SupplierManagementModel ()
        {
            __SupplierController = new SupplierController();
            __ProductController = new ProductController();
            __AuditLogController = new AuditLogController();
            __UserPermissionController = new UserPermissionController();
        }

        internal int GetSupplierProductCount (Guid uid)
        {
            return __ProductController
                .GetAll()
                .Where(product => product.Supplier_UID == uid)
                .Count();
        }

        public bool IsSupplierInUse (Guid uid)
        {
            return __SupplierController.IsSupplierInUse(uid);
        }

        public IActionResult OnGet ()
        {
            string _CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserPermissions _CurrentUserPermissions = __UserPermissionController.GetPermissions(new Guid(_CurrentUserID));

            if (_CurrentUserPermissions.ManageSuppliers)
            {
                Suppliers = __SupplierController.GetAll();
                return Page();
            }
            else
            {
                return RedirectToPage(ACCESS_ERROR_PAGE_PATH);
            }
        }

        public async Task<IActionResult> OnGetDeleteSupplier (Guid uid)
        {
            if (__SupplierController.IsSupplierInUse(uid))
            {
                ModelState.AddModelError("", "Supplier in use, can't delete!");
                return Page();
            }
            else
            {
                string? _SupplierName = __SupplierController.Get(uid).Name;
                __AuditLogController.Create(User.Identity.Name, string.Format(LOG_ENTRY_DELETE, _SupplierName), LogType.SupplierDelete);

                __SupplierController.Delete(uid);
                return RedirectToPage("SupplierManagement");
            }
        }

        public List<Supplier> Suppliers { get; set; }
    }
}
