using FutureFridges.Business.StockManagement;
using FutureFridges.Business.HealthReport;
using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using FutureFridges.Business.OrderManagement;
using Microsoft.IdentityModel.Tokens;
using FutureFridges.Business.Email;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
        private readonly IEmailManager __EmailManager;

        public HealthReportModel ()
        {
            __StockItemController = new StockItemController();
            __ProductController = new ProductController();
            __UserPermissionController = new UserPermissionController();
            __HealthReportController = new HealthReportController();
            __EmailManager = new EmailManager();
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

        private void ValidateModel()
        {
            if (SafetyOfficerEmail.IsNullOrEmpty())
            {
                ModelState.AddModelError("SafetyOfficerEmail", "Email is required!");
            }
            if (!SafetyOfficerEmail.IsNullOrEmpty() && !__EmailManager.IsValidEmail(SafetyOfficerEmail))
            {
                ModelState.AddModelError("SafetyOfficerEmail", "Email address is invalid!");
            }
        }

        public IActionResult OnPostSendReport(string safetyOfficerEmail)
        {
            ModelState.Clear();
            ValidateModel();
            if (ModelState.ErrorCount > 0)
            {
                SetStockAndProducts();
                
                return Page();
            }
            
            __HealthReportController.CreateHealthReport(safetyOfficerEmail, DateTime.Now);
            return RedirectToPage("HealthReport", new { SuccessfulEmail = true });
        }

        public UserPermissions CurrentUserPermissions { get; set; }
        public List<Product> Products { get; set; }
        public List<StockItem> StockItems { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string? SafetyOfficerEmail { get; set; }
    }
}
