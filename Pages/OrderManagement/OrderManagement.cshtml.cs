using FutureFridges.Business.OrderManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutureFridges.Pages.OrderManagement
{
    public class OrderManagementModel : PageModel
    {
        private readonly IOrderController __OrderController;
        private readonly ISupplierController __SupplierController;

        public OrderManagementModel() {

            __OrderController= new OrderController();
            __SupplierController= new SupplierController(); 
        }

        public void OnGet()
        {
            Orders = __OrderController.GetAll();
        }

        internal string GetSupplierName (Guid uid)
        {
            return __SupplierController.Get(uid).Name;
        }

        [BindProperty(SupportsGet = true)]
        public List<Order> Orders { get; set; }
    }
}
