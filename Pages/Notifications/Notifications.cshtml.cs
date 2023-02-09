using FutureFridges.Business.Enums;
using FutureFridges.Business.Notifications;
using FutureFridges.Business.StockManagement;
using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutureFridges.Pages.Notifications
{
    public class NotificationsModel : PageModel
    {
        private readonly INotificationController __NotificationController;
        private readonly UserManager<FridgeUser> __UserManager;
        
        public NotificationsModel(UserManager<FridgeUser> userManager)
        {
            __NotificationController = new NotificationController(userManager);
            __UserManager = userManager;
        }
        
        public void OnGet()
        {
            Guid _User_UID = new Guid(__UserManager.GetUserId(User));

            Notifications = __NotificationController.GetByUser(_User_UID);
        }

        public async Task<IActionResult> OnGetDeleteNotification (Guid uid)
        {
            __NotificationController.Delete(uid);

            return RedirectToPage("Notifications");
        }

        public List<Notification> Notifications { get; set; }
    }
}
