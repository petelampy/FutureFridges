using FutureFridges.Business.OrderManagement;
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

        private readonly IOrderController __OrderController;

        public LoginModel (SignInManager<FridgeUser> signInManager)
        {
            __SignInManager = signInManager;
            __OrderController = new OrderController();
        }

        public void OnGetAsync ()
        {
        }

        public async Task<IActionResult> OnPostLoginAsync (string username, string password, bool rememberMe, string? returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult _IdentityResult = await __SignInManager
                    .PasswordSignInAsync(username, password, rememberMe, false);

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
                    ModelState.AddModelError("LoginError", "Incorrect Username or Password!");
                }
            }

            return Page();
        }

        public IActionResult OnPostPinCodeLogin (int pinCode)
        {
            if (!__OrderController.IsValidOrderPinCode(pinCode))
            {
                ModelState.AddModelError("PinError", "Invalid Pin Code");
                return Page();
            }
            else
            {
                return RedirectToPage("../Deliveries/ReviewDelivery", new { PinCode = pinCode });
            }
        }
    }
}
