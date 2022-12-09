using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutureFridges.Pages.UserManagement
{
    [Authorize]
    public class UserManagementModel : PageModel
    {
        public void OnGet ()
        {
            UserController _UserController = new UserController();
            Users = _UserController.GetAll();
        }

        public List<FridgeUser> Users { get; set; }
    }
}
