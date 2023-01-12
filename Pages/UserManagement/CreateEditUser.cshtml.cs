using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FutureFridges.Pages.UserManagement
{
    [Authorize]
    public class CreateEditUserModel : PageModel
    {
        private const string ACCESS_ERROR_PAGE_PATH = "../Account/AccessError";

        private readonly UserController __UserController;
        private readonly UserPermissionController __UserPermissionController;

        public CreateEditUserModel ()
        {
            __UserPermissionController = new UserPermissionController();
            __UserController = new UserController();
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

        public IActionResult OnPost ()
        {
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


        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        [BindProperty]
        public FridgeUser ManagedUser { get; set; }

        [BindProperty]
        public UserPermissions ManagedUserPermissions { get; set; }
    }
}
