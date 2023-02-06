using FutureFridges.Business.AuditLog;
using FutureFridges.Business.Email;
using FutureFridges.Business.Enums;
using FutureFridges.Business.OrderManagement;
using FutureFridges.Business.StockManagement;
using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace FutureFridges.Pages.UserManagement
{
    [Authorize]
    public class CreateEditUserModel : PageModel
    {
        private const string ACCESS_ERROR_PAGE_PATH = "../Account/AccessError";
        private const string LOG_CREATE_FORMAT = "{0} was created";
        private const string LOG_RENAME_FORMAT = "{0} was renamed to {1}";
        private const string LOG_CHANGE_EMAIL_FORMAT = "{0}'s email was changed from {1} to {2}";
        private const string LOG_CHANGE_TYPE_FORMAT = "{0}'s user type was changed from {1} to {2}";

        private readonly IUserController __UserController;
        private readonly IUserPermissionController __UserPermissionController;
        private readonly IAuditLogController __AuditLogController;
        private readonly IEmailManager __EmailManager;

        public CreateEditUserModel (UserManager<FridgeUser> userManager)
        {
            __UserPermissionController = new UserPermissionController();
            __UserController = new UserController(userManager);
            __AuditLogController = new AuditLogController();
            __EmailManager = new EmailManager();
        }

        //MAYBE ADD AN ON GET FLAG TO DETERMINE IF IT'S A CREATE/EDIT RATHER THAN RELYING ON THE UID.
        //THIS WOULD ALSO ALLOW FOR A "VIEW" MODE WHERE ALL SAVE BUTTONS AND FIELDS ARE DISABLED JUST FOR VIEWING A PRODUCT
        public IActionResult OnGet ()
        {
            string _CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserPermissions _CurrentUserPermissions = __UserPermissionController.GetPermissions(new Guid(_CurrentUserID));

            if (_CurrentUserPermissions.ManageUser)
            {
                //CONVERT THE ID FIELD IN THE USERS DATABASE TO A GUID TO STOP CONVERT BEING NEEDED? BLAME ASP.NET
                if (Id != null && new Guid(Id) != Guid.Empty)
                {
                    ManagedUser = __UserController.GetUser(Id);
                    ManagedUserPermissions = __UserPermissionController.GetPermissions(new Guid(Id));
                }
                else
                {
                    ManagedUser = new FridgeUser();
                }

                return Page();

            }
            else
            {
                return RedirectToPage(ACCESS_ERROR_PAGE_PATH);
            }
        }

        private void CreateAuditLogs()
        {
            if (Id == null || new Guid(Id) == Guid.Empty)
            {
                __AuditLogController.Create(User.Identity.Name,
                    string.Format(LOG_CREATE_FORMAT, ManagedUser.UserName),
                    LogType.UserCreate);
            }
            else
            {
                FridgeUser _CurrentUser = __UserController.GetUser(Id);

                if (_CurrentUser.UserName != ManagedUser.UserName)
                {
                    __AuditLogController.Create(User.Identity.Name,
                    string.Format(LOG_RENAME_FORMAT, _CurrentUser.UserName, ManagedUser.UserName),
                    LogType.UserUpdate);
                }
                if (_CurrentUser.Email != ManagedUser.Email)
                {
                    __AuditLogController.Create(User.Identity.Name,
                    string.Format(LOG_CHANGE_EMAIL_FORMAT, _CurrentUser.UserName, _CurrentUser.Email, ManagedUser.Email),
                    LogType.UserUpdate);
                }
                if (_CurrentUser.UserType != ManagedUser.UserType)
                {
                    __AuditLogController.Create(User.Identity.Name,
                    string.Format(LOG_CHANGE_TYPE_FORMAT, _CurrentUser.UserName, _CurrentUser.UserType, ManagedUser.UserType),
                    LogType.UserUpdate);
                }
            }

        }

        public IActionResult OnPost ()
        {
            ModelState.Remove("ManagedUserPermissions.User_UID");
            ValidateModel();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            CreateAuditLogs();

            if (Id != null && new Guid(Id) != Guid.Empty)
            {
                __UserController.UpdateUser(ManagedUser);
                __UserPermissionController.UpdatePermissions(ManagedUserPermissions);
            }
            else
            {
                __UserController.CreateUser(ManagedUser);
            }

            return RedirectToPage("UserManagement");
        }

        private void ValidateModel ()
        {
            if (ManagedUser.UserName.IsNullOrEmpty())
            {
                ModelState.AddModelError("ManagedUser.UserName", "User Name is required!");
            }
            if (ManagedUser.Email.IsNullOrEmpty())
            {
                ModelState.AddModelError("ManagedUser.Email", "Email is required!");
            }

            if(!ManagedUser.Email.IsNullOrEmpty() && !__EmailManager.IsValidEmail(ManagedUser.Email))
            {
                ModelState.AddModelError("ManagedUser.Email", "Email address is invalid");
            }

            if (Id == null || new Guid(Id) == Guid.Empty)
            {
                if(!ManagedUser.Email.IsNullOrEmpty() && __UserController.IsEmailInUse(ManagedUser.Email))
                {
                    ModelState.AddModelError("ManagedUser.Email", "Email is already in use!");
                }

                if (!ManagedUser.UserName.IsNullOrEmpty() && __UserController.IsUsernameInUse(ManagedUser.UserName))
                {
                    ModelState.AddModelError("ManagedUser.UserName", "User Name is already in use!");
                }
            }
            else
            {
                FridgeUser _CurrentUser = __UserController.GetUser(ManagedUser.Id);

                if (_CurrentUser.Email != ManagedUser.Email && !ManagedUser.Email.IsNullOrEmpty() && __UserController.IsEmailInUse(ManagedUser.Email))
                {
                    ModelState.AddModelError("ManagedUser.Email", "Email is already in use!");
                }

                if (_CurrentUser.UserName != ManagedUser.UserName && !ManagedUser.UserName.IsNullOrEmpty() && __UserController.IsUsernameInUse(ManagedUser.UserName))
                {
                    ModelState.AddModelError("ManagedUser.UserName", "User Name is already in use!");
                }
            }
        }

        [BindProperty(SupportsGet = true)]
        public string? Id { get; set; }

        [BindProperty]
        public FridgeUser ManagedUser { get; set; }

        [BindProperty]
        public UserPermissions ManagedUserPermissions { get; set; }
    }
}
