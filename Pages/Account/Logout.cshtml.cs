using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutureFridges.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private const string INDEX_PAGE_PATH = "../Index";

        private readonly SignInManager<FridgeUser> __SignInManager;

        public LogoutModel (SignInManager<FridgeUser> signInManager)
        {
            __SignInManager = signInManager;
        }

        public IActionResult OnGetAsync ()
        {
            __SignInManager.SignOutAsync();
            return RedirectToPage(INDEX_PAGE_PATH);
        }
    }
}
