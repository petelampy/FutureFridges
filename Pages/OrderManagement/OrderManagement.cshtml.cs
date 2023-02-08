using FutureFridges.Business.OrderManagement;
using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FutureFridges.Pages.OrderManagement
{
    public class OrderManagementModel : PageModel
    {
        private const string ACCESS_ERROR_PAGE_PATH = "../Account/AccessError";

        private readonly IOrderController __OrderController;
        private readonly ISupplierController __SupplierController;
        private readonly IUserPermissionController __UserPermissionController;

        public OrderManagementModel() {

            __OrderController= new OrderController();
            __SupplierController= new SupplierController(); 
            __UserPermissionController = new UserPermissionController();
        }

        public IActionResult OnGet()
        {
            string _CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            CurrentUserPermissions = __UserPermissionController.GetPermissions(new Guid(_CurrentUserID));

            if(CurrentUserPermissions.ManageOrders)
            {
                __OrderController.ClearUnfinishedOrders();
                
                Orders = __OrderController.GetAll();
                return Page();
            }
            else
            {
                return RedirectToPage(ACCESS_ERROR_PAGE_PATH);
            }
            
        }

        internal string GetSupplierName (Guid uid)
        {
            return __SupplierController.Get(uid).Name;
        }

        [BindProperty(SupportsGet = true)]
        public List<Order> Orders { get; set; }

        [BindProperty(SupportsGet = true)]
        public UserPermissions CurrentUserPermissions { get; set; }
    }
}
