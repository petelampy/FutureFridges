using FutureFridges.Business.AuditLog;
using FutureFridges.Business.Enums;
using FutureFridges.Business.OrderManagement;
using FutureFridges.Business.StockManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutureFridges.Pages.SupplierManagement
{
    public class SupplierManagementModel : PageModel
    {
        private const string LOG_ENTRY_DELETE = "{0} was deleted";

        private readonly IProductController __ProductController;
        private readonly ISupplierController __SupplierController;
        private readonly IAuditLogController __AuditLogController;

        public SupplierManagementModel ()
        {
            __SupplierController = new SupplierController();
            __ProductController = new ProductController();
            __AuditLogController = new AuditLogController();
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
            Suppliers = __SupplierController.GetAll();

            return Page();
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
