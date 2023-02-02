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

        private readonly IAuditLogController __AuditLogController;
        private readonly IUserController __UserController;

        public AuditLogModel (UserManager<FridgeUser> userManager)
        {
            __AuditLogController = new AuditLogController();
            __UserController = new UserController(userManager);
        }

        public IActionResult OnGet ()
        {
            string _CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            CurrentUser = __UserController.GetUser(_CurrentUserID);

            if(CurrentUser.UserType == UserType.Administrator)
            {
                Logs = __AuditLogController.GetAll();
            }
            else
            {
                Logs = __AuditLogController.GetStockLogs();
            }

            return Page();
        }

        public List<LogEntry> Logs { get; set; }
        [BindProperty(SupportsGet = true)]
        public FridgeUser CurrentUser { get; set; }
    }
}
