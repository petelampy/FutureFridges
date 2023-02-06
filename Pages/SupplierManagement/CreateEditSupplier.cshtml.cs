using FutureFridges.Business.AuditLog;
using FutureFridges.Business.Email;
using FutureFridges.Business.Enums;
using FutureFridges.Business.OrderManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace FutureFridges.Pages.SupplierManagement
{
    public class CreateEditSupplierModel : PageModel
    {
        private const string LOG_CREATE_FORMAT = "{0} was created";
        private const string LOG_UPDATE_EMAIL = "{0}'s email was changed from {1} to {2}";
        private const string LOG_UPDATE_NAME = "{0} was renamed to {1}";

        private readonly IAuditLogController __AuditLogController;
        private readonly ISupplierController __SupplierController;
        private readonly IEmailManager __EmailManager;
        
        public CreateEditSupplierModel ()
        {
            __SupplierController = new SupplierController();
            __AuditLogController = new AuditLogController();
            __EmailManager = new EmailManager();
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
            ValidateModel();
            if (ModelState.ErrorCount > 0)
            {
                return Page();
            }

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

        private void ValidateModel()
        {
            if (Supplier.Name.IsNullOrEmpty())
            {
                ModelState.AddModelError("Supplier.Name", "Supplier name is required!");
            }
            if (Supplier.Email.IsNullOrEmpty())
            {
                ModelState.AddModelError("Supplier.Email", "Email is required!");
            }
            if (!Supplier.Email.IsNullOrEmpty() && !__EmailManager.IsValidEmail(Supplier.Email))
            {
                ModelState.AddModelError("Supplier.Email", "Email address is invalid");
            }

            Supplier _CurrentSupplier = __SupplierController.Get(UID);
            
            if (!Supplier.Name.IsNullOrEmpty() && _CurrentSupplier.Name != Supplier.Name && __SupplierController.IsNameInUse(Supplier.Name))
            {
                ModelState.AddModelError("Supplier.Name", "Supplier name is in use!");
            }
            if (!Supplier.Email.IsNullOrEmpty() && _CurrentSupplier.Email != Supplier.Email && __SupplierController.IsEmailInUse(Supplier.Email))
            {
                ModelState.AddModelError("Supplier.Email", "Email is in use!");
            }
        }

        [BindProperty]
        public Supplier Supplier { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid UID { get; set; }
    }
}
