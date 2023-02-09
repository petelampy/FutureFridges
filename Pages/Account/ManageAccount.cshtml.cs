using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Text;

namespace FutureFridges.Pages.Account
{
    [Authorize]
    public class ManageAccountModel : PageModel
    {
        private const string INDEX_PAGE_PATH = "../Index";
        private const string PASSWORD_MISMATCH_ERROR_CODE = "PasswordMismatch";

        private readonly UserManager<FridgeUser> __UserManager;

        private readonly List<string> __NewPasswordErrorCodes = new List<string>{"PasswordTooShort",
            "PasswordRequiresNonAlphanumeric",
            "PasswordRequiresLower",
            "PasswordRequiresDigit",
            "PasswordRequiresUniqueChars",
            "PasswordRequiresUpper"};

        public ManageAccountModel (UserManager<FridgeUser> userManager)
        {
            __UserManager = userManager;
        }

        public void DisplayPasswordErrors (IdentityResult result)
        {

            StringBuilder _StringBuilder = new StringBuilder();

            foreach (IdentityError _Error in result.Errors)
            {
                if (__NewPasswordErrorCodes.Contains(_Error.Code))
                {
                    _StringBuilder.Append("\n" + _Error.Description);
                }

                if (_Error.Code == PASSWORD_MISMATCH_ERROR_CODE)
                {
                    ModelState.AddModelError("currentpassword", _Error.Description);
                }
            }

            if (_StringBuilder.Length > 0)
            {
                ModelState.AddModelError("newpassword", _StringBuilder.ToString());
            }
        }

        public IActionResult OnGet ()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostChangePasswordAsync (string currentPassword, string newPassword)
        {
            if (ModelState.IsValid)
            {
                string _CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                FridgeUser _CurrentUser = await __UserManager.FindByIdAsync(_CurrentUserID);

                IdentityResult _Result = await __UserManager.ChangePasswordAsync(_CurrentUser, currentPassword, newPassword);

                if (_Result.Succeeded)
                {
                    return RedirectToPage(INDEX_PAGE_PATH);
                }
                else
                {
                    DisplayPasswordErrors(_Result);

                    return Page();
                }
            }
            else
            {
                return Page();
            }
        }
    }
}
