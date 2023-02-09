using FutureFridges.Business.Notifications;
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
        private readonly INotificationController __NotificationController;

        public LoginModel (SignInManager<FridgeUser> signInManager, UserManager<FridgeUser> userManager)
        {
            __SignInManager = signInManager;
            __OrderController = new OrderController();
            __NotificationController = new NotificationController(userManager);
        }

        public IActionResult OnGetAsync ()
        {
            if (__SignInManager.IsSignedIn(User))
            {
                return RedirectToPage(INDEX_PAGE_PATH);
            }
            else
            {
                return Page();
            }
        }

        public async Task<IActionResult> OnPostLoginAsync (string username, string password, bool rememberMe, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult _IdentityResult = await __SignInManager
                    .PasswordSignInAsync(username, password, rememberMe, false);

                if (_IdentityResult.Succeeded)
                {
                    __NotificationController.CreateNotifications();

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
