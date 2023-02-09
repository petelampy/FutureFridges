using FutureFridges.Business.StockManagement;
using FutureFridges.Business.HealthReport;
using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FutureFridges.Pages.HealthReport
{
    [Authorize]
    public class HealthReportModel : PageModel
    {
        private const string ACCESS_ERROR_PAGE_PATH = "../Account/AccessError";

        private readonly IProductController __ProductController;
        private readonly IStockItemController __StockItemController;
        private readonly IUserPermissionController __UserPermissionController;
        private readonly IHealthReportController __HealthReportController;

        public HealthReportModel ()
        {
            __StockItemController = new StockItemController();
            __ProductController = new ProductController();
            __UserPermissionController = new UserPermissionController();
            __HealthReportController = new HealthReportController();
        }

        public IActionResult OnGet ()
        {
            SetUserPermissions();

            if (CurrentUserPermissions.ManageHealthAndSafetyReport)
            {
                SetStockAndProducts();
                return Page();
            }
            else
            {
                return RedirectToPage(ACCESS_ERROR_PAGE_PATH);
            }
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

        public IActionResult OnPostSendReport(string safetyOfficerEmail)
        {
            if (!safetyOfficerEmail.IsNullOrEmpty())
            {
                try
                {
                    var _Email = new MailAddress(safetyOfficerEmail);
                    __HealthReportController.CreateHealthReport(safetyOfficerEmail, DateTime.Now);
                } catch {}
            }

            return RedirectToPage("HealthReport");
        }

        public UserPermissions CurrentUserPermissions { get; set; }
        public List<Product> Products { get; set; }
        public List<StockItem> StockItems { get; set; }
    }
}
