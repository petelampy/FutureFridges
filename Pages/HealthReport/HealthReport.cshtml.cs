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

        private readonly ProductController __ProductController;
        private readonly StockItemController __StockItemController;
        private readonly UserPermissionController __UserPermissionController;

        public HealthReportModel ()
        {
            __StockItemController = new StockItemController();
            __ProductController = new ProductController();
            __UserPermissionController = new UserPermissionController();
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

        public IActionResult OnPostSendReport(string email)
        {
            Console.WriteLine(email);
            HealthReportController _HealthReportController = new HealthReportController();
            _HealthReportController.createHealthReport(email, DateTime.Now);
            return RedirectToPage("HealthReport");
        }

        public UserPermissions CurrentUserPermissions { get; set; }
        public List<Product> Products { get; set; }
        public List<StockItem> StockItems { get; set; }
    }
}
