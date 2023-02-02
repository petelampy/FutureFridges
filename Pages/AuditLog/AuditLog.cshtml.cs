using FutureFridges.Business.AuditLog;
using FutureFridges.Business.Enums;
using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FutureFridges.Pages.Logs
{
    public class AuditLogModel : PageModel
    {
        private const string ACCESS_ERROR_PAGE_PATH = "../Account/AccessError";

        private readonly IAuditLogController __AuditLogController;
        private readonly IUserController __UserController;
        private readonly IUserPermissionController __UserPermissionController;

        public AuditLogModel (UserManager<FridgeUser> userManager)
        {
            __AuditLogController = new AuditLogController();
            __UserController = new UserController(userManager);
            __UserPermissionController = new UserPermissionController();
        }

        public IActionResult OnGet ()
        {
            string _CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            CurrentUser = __UserController.GetUser(_CurrentUserID);
            UserPermissions _CurrentUserPermissions = __UserPermissionController.GetPermissions(new Guid(_CurrentUserID));

            if (_CurrentUserPermissions.ViewAuditLog)
            {
                if (CurrentUser.UserType == UserType.Administrator)
                {
                    Logs = __AuditLogController.GetAll();
                }
                else
                {
                    Logs = __AuditLogController.GetStockLogs();
                }

                return Page();
            }
            else
            {
                return RedirectToPage(ACCESS_ERROR_PAGE_PATH);
            }

        }

        [BindProperty(SupportsGet = true)]
        public FridgeUser CurrentUser { get; set; }

        public List<LogEntry> Logs { get; set; }
    }
}
