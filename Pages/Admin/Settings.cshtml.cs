using FutureFridges.Business.Admin;
using FutureFridges.Business.Enums;
using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FutureFridges.Pages.Admin
{
    public class SettingsModel : PageModel
    {
        private readonly ISettingsController __SettingsController;
        private readonly IUserController __UserController;

        public SettingsModel (UserManager<FridgeUser> userManager)
        {
            __SettingsController = new SettingsController();
            __UserController = new UserController(userManager);
        }

        private void CreateAdminSelector ()
        {
            List<FridgeUser> _Administrators = __UserController
                .GetAll()
                .Where(user => user.UserType == UserType.Administrator)
                .ToList();

            AdminSelection = _Administrators.Select(admin =>
                new SelectListItem
                {
                    Text = admin.UserName,
                    Value = admin.Id,
                    Selected = Settings.Administrator_UID.Equals(new Guid(admin.Id))
                }).ToList();
        }

        public void OnGet ()
        {
            Settings = __SettingsController.Get();

            CreateAdminSelector();
        }

        public List<SelectListItem> AdminSelection { get; set; }
        public Settings Settings { get; set; }
    }
}
