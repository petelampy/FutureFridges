using FutureFridges.Business.StockManagement;
using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutureFridges.Pages.UserManagement
{
    public class UserManagementModel : PageModel
    {
        public List<FridgeUser> Users { get; set; }

        public void OnGet ()
        {
            UserController _UserController = new UserController();
            Users = _UserController.GetAll();
        }
    }
}
