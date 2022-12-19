using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutureFridges.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private const string INDEX_PAGE_PATH = "../Index"; //ADD TO GLOBAL RESX FILE

        private readonly SignInManager<FridgeUser> __SignInManager;

        public LogoutModel(SignInManager<FridgeUser> signInManager)
        {
            __SignInManager = signInManager;
        }

        public IActionResult OnGetAsync ()
        {
            //THIS COULD BE CHANGED TO NOT NEED A PAGE, ADDED AS A PAGE FOR EASY TESTING
            __SignInManager.SignOutAsync();
            return RedirectToPage(INDEX_PAGE_PATH);
        }
    }
}
