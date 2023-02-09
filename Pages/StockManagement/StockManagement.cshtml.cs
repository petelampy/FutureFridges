using FutureFridges.Business.AuditLog;
using FutureFridges.Business.Enums;
using FutureFridges.Business.Notifications;
using FutureFridges.Business.StockManagement;
using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FutureFridges.Pages.StockManagement
{
    [Authorize]
    public class StockManagementModel : PageModel
    {
        private const string ACCESS_ERROR_PAGE_PATH = "../Account/AccessError";
        private const string LOG_TAKE_FORMAT = "A {0} was removed from the fridge";

        private readonly IAuditLogController __AuditLogController;
        private readonly INotificationController __NotificationController;
        private readonly IProductController __ProductController;
        private readonly IStockItemController __StockItemController;
        private readonly IUserPermissionController __UserPermissionController;

        public StockManagementModel (UserManager<FridgeUser> userManager)
        {
            __StockItemController = new StockItemController();
            __ProductController = new ProductController();
            __UserPermissionController = new UserPermissionController();
            __AuditLogController = new AuditLogController();
            __NotificationController = new NotificationController(userManager);
        }

        public IActionResult OnGet ()
        {
            SetUserPermissions();

            if (CurrentUserPermissions.ViewStock)
            {
                SetStockAndProducts();
                return Page();
            }
            else
            {
                return RedirectToPage(ACCESS_ERROR_PAGE_PATH);
            }
        }

        public async Task<IActionResult> OnGetTakeProduct (Guid uid)
        {
            StockItem _CurrentItem = __StockItemController.GetStockItem(uid);

            string _CurrentProductName = __ProductController.GetProduct(_CurrentItem.Product_UID).Name;
            __AuditLogController.Create(User.Identity.Name, string.Format(LOG_TAKE_FORMAT, _CurrentProductName), LogType.ItemTake);

            __StockItemController.DeleteStockItem(uid);
            __NotificationController.CreateProductNotification(_CurrentItem.Product_UID);

            SetStockAndProducts();
            SetUserPermissions();

            int _RemainingQuantity = StockItems.Where(stockItem => stockItem.Product_UID == _CurrentItem.Product_UID).Count();
            if (_RemainingQuantity > 0)
            {
                SelectedProduct = __ProductController.GetProduct(_CurrentItem.Product_UID);
                return Page();
            }
            else
            {
                return RedirectToPage("StockManagement");
            }
        }

        public void OnPostSelectProduct (Guid uid)
        {
            SetUserPermissions();
            SetStockAndProducts();

            SelectedProduct = __ProductController.GetProduct(uid);
        }

        public void SetStockAndProducts ()
        {
            StockItems = __StockItemController.GetAll();
            Products = __ProductController.GetAll();
        }

        public void SetUserPermissions ()
        {
            string _CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            CurrentUserPermissions = __UserPermissionController.GetPermissions(new Guid(_CurrentUserID));
        }

        [BindProperty(SupportsGet = true)]
        public UserPermissions CurrentUserPermissions { get; set; }

        public string ProductImagePath { get; set; } = "../Images/Products/";
        public List<Product> Products { get; set; }
        public Product? SelectedProduct { get; set; } = new Product();
        public List<StockItem> StockItems { get; set; }
    }
}
