using FutureFridges.Business.StockManagement;
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

        private readonly IUserPermissionController __UserPermissionController;
        private readonly IUserController __UserController;
        private readonly UserManager<FridgeUser> __UserManager;

        public UserManagementModel (UserManager<FridgeUser> userManager)
        {
            __UserPermissionController = new UserPermissionController();
            __UserController = new UserController(userManager);
            __UserManager = userManager;
        }

        public IActionResult OnGet ()
        {
            string _CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserPermissions _CurrentUserPermissions = __UserPermissionController.GetPermissions(new Guid(_CurrentUserID));

            if (_CurrentUserPermissions.ManageUser)
            {
                UserController _UserController = new UserController(__UserManager);
                Users = _UserController
                    .GetAll()
                    .Where(user => user.Id != _CurrentUserID)
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

            __UserController.DeleteUser(id);

            return RedirectToPage("UserManagement");
        }

        public async Task<IActionResult> OnGetResetPassword (string id)
        {

            await __UserController.ResetPassword(id);

            return RedirectToPage("UserManagement");
        }

        public List<FridgeUser> Users { get; set; }
    }
}
