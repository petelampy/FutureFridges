using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace FutureFridges.Pages.Account
{
    public class LoginModel : PageModel
    {
        private const string INDEX_PAGE_PATH = "../Index";

        private readonly SignInManager<FridgeUser> __SignInManager;

        public LoginModel (SignInManager<FridgeUser> signInManager)
        {
            __SignInManager = signInManager;
        }

        public void OnGetAsync ()
        {
        }

        public async Task<IActionResult> OnPostAsync (string? returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult _IdentityResult = await __SignInManager
                    .PasswordSignInAsync(Username, Password, RememberMe, false);

                if (_IdentityResult.Succeeded)
                {
                    if (returnUrl == null || returnUrl == "/")
                    {
                        return RedirectToPage(INDEX_PAGE_PATH);
                    }
                    else
                    {
                        return RedirectToPage(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password!");
                }
            }

            return Page();
        }

        [BindProperty]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter a password!")]
        public string Password { get; set; }

        [BindProperty]
        public bool RememberMe { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter a username!")]
        public string Username { get; set; }
    }
}
