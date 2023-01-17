using FutureFridges.Business.StockManagement;
using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FutureFridges.Pages.StockManagement
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

        public async Task<IActionResult> OnGetTakeProduct (Guid uid)
        {
            StockItem _CurrentItem = __StockItemController.GetStockItem(uid);
            __StockItemController.DeleteStockItem(uid);

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

        public UserPermissions CurrentUserPermissions { get; set; }
        public string ProductImagePath { get; set; } = "../Images/Products/";
        public List<Product> Products { get; set; }
        public Product? SelectedProduct { get; set; } = new Product();
        public List<StockItem> StockItems { get; set; }
    }
}
