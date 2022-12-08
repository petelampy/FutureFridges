using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutureFridges.Pages.UserManagement
{
    public class CreateEditUserModel : PageModel
    {
        public void OnGet ()
        {
            //MAYBE ADD AN ON GET FLAG TO DETERMINE IF IT'S A CREATE/EDIT RATHER THAN RELYING ON THE UID.
            //THIS WOULD ALSO ALLOW FOR A "VIEW" MODE WHERE ALL SAVE BUTTONS AND FIELDS ARE DISABLED JUST FOR VIEWING A PRODUCT


            //CONVERT THE ID FIELD IN THE USERS DATABASE TO A GUID TO STOP CONVERT BEING NEEDED? BLAME ASP.NET
            if (Id != null && new Guid(Id) != Guid.Empty)
            {
                UserController _UserController = new UserController();
                User = _UserController.GetUser(Id);
            }
            else
            {
                User = new FridgeUser();
            }
        }

        public IActionResult OnPost ()
        {
            //MAKE THE USERCONTROLLER WORK AS A CLASS VARIABLE SO IT CAN BE USED WITHOUT REDECLARING?
            UserController _UserController = new UserController();

            if (Id != null && new Guid(Id) != Guid.Empty)
            {
                _UserController.UpdateUser(User);
            }
            else
            {
                _UserController.CreateUser(User);
            }

            return RedirectToPage("UserManagement");
        }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        [BindProperty]
        public FridgeUser User { get; set; }
    }
}
