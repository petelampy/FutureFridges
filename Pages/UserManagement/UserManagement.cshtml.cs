using FutureFridges.Business.AuditLog;
using FutureFridges.Business.Enums;
using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FutureFridges.Pages.UserManagement
{
    [Authorize]
    public class UserManagementModel : PageModel
    {
        private const string ACCESS_ERROR_PAGE_PATH = "../Account/AccessError";
        private const string LOG_DELETE_FORMAT = "{0} was deleted";
        private const string LOG_PASSWORD_RESET_FORMAT = "{0}'s password was reset";

        private readonly IAuditLogController __AuditLogController;
        private readonly IUserController __UserController;
        private readonly UserManager<FridgeUser> __UserManager;
        private readonly IUserPermissionController __UserPermissionController;

        public UserManagementModel (UserManager<FridgeUser> userManager)
        {
            __UserPermissionController = new UserPermissionController();
            __UserController = new UserController(userManager);
            __AuditLogController = new AuditLogController();
            __UserManager = userManager;
        }

        public IActionResult OnGet ()
        {
            CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserPermissions _CurrentUserPermissions = __UserPermissionController.GetPermissions(new Guid(CurrentUserID));

            if (_CurrentUserPermissions.ManageUser)
            {
                Users = __UserController
                    .GetAll()
                    .Where(user => user.Id != CurrentUserID)
                    .ToList();

                return Page();
            }
            else
            {
                return RedirectToPage(ACCESS_ERROR_PAGE_PATH);
            }
        }

        public async Task<IActionResult> OnGetDeleteUser (string id)
        {
            string _CurrentUserName = __UserController.GetUser(id).UserName;
            __AuditLogController.Create(User.Identity.Name, string.Format(LOG_DELETE_FORMAT, _CurrentUserName), LogType.UserDelete);

            __UserController.DeleteUser(id);

            return RedirectToPage("UserManagement");
        }

        public async Task OnGetResetPassword (string id, string currentUserID)
        {
            string _CurrentUserName = __UserController.GetUser(id).UserName;
            __AuditLogController.Create(User.Identity.Name, string.Format(LOG_PASSWORD_RESET_FORMAT, _CurrentUserName), LogType.UserPasswordReset);

            await __UserController.ResetPassword(id);

            Users = __UserController
                    .GetAll()
                    .Where(user => user.Id != currentUserID)
                    .ToList();
        }

        [BindProperty(SupportsGet = true)]
        public string CurrentUserID { get; set; }
        public List<FridgeUser> Users { get; set; }
    }
}
