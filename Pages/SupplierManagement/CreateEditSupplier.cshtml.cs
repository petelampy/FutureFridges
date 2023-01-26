using FutureFridges.Business.AuditLog;
using FutureFridges.Business.Enums;
using FutureFridges.Business.OrderManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutureFridges.Pages.SupplierManagement
{
    public class CreateEditSupplierModel : PageModel
    {
        private const string LOG_CREATE_FORMAT = "{0} was created";
        private const string LOG_UPDATE_EMAIL = "{0}'s email was changed from {1} to {2}";
        private const string LOG_UPDATE_NAME = "{0} was renamed to {1}";

        private readonly IAuditLogController __AuditLogController;
        private readonly ISupplierController __SupplierController;

        public CreateEditSupplierModel ()
        {
            __SupplierController = new SupplierController();
            __AuditLogController = new AuditLogController();
        }

        private void CreateAuditLogs ()
        {
            if (UID == Guid.Empty)
            {
                __AuditLogController.Create(User.Identity.Name,
                    string.Format(LOG_CREATE_FORMAT, Supplier.Name),
                    LogType.SupplierCreate);
            }
            else
            {
                Supplier _CurrentSupplier = __SupplierController.Get(UID);

                if (_CurrentSupplier.Name != Supplier.Name)
                {
                    __AuditLogController.Create(User.Identity.Name,
                    string.Format(LOG_UPDATE_NAME, _CurrentSupplier.Name, Supplier.Name),
                    LogType.SupplierUpdate);
                }

                if (_CurrentSupplier.Email != Supplier.Email)
                {
                    __AuditLogController.Create(User.Identity.Name,
                    string.Format(LOG_UPDATE_EMAIL, _CurrentSupplier.Name, _CurrentSupplier.Email, Supplier.Email),
                    LogType.SupplierUpdate);
                }
            }
        }

        public IActionResult OnGet ()
        {
            if (UID != Guid.Empty)
            {
                Supplier = __SupplierController.Get(UID);

            }
            else
            {
                Supplier = new Supplier();
            }

            return Page();
        }

        public IActionResult OnPost ()
        {
            //ADD VALIDATION HERE

            CreateAuditLogs();

            if (UID != Guid.Empty)
            {
                __SupplierController.Update(Supplier);
            }
            else
            {
                __SupplierController.Create(Supplier);
            }

            return RedirectToPage("SupplierManagement");
        }

        [BindProperty]
        public Supplier Supplier { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid UID { get; set; }
    }
}
