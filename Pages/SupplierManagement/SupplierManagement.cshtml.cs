using FutureFridges.Business.OrderManagement;
using FutureFridges.Business.StockManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutureFridges.Pages.SupplierManagement
{
    public class SupplierManagementModel : PageModel
    {
        private readonly ISupplierController __SupplierController;
        private readonly IProductController __ProductController;

        public SupplierManagementModel ()
        {
            __SupplierController = new SupplierController();
            __ProductController = new ProductController(); 
        }

        public IActionResult OnGet()
        {
            Suppliers = __SupplierController.GetAll();

            return Page();
        }

        internal int GetSupplierProductCount(Guid uid) {

            return __ProductController
                .GetAll()
                .Where(product => product.Supplier_UID == uid)
                .Count();
        }

        public List<Supplier> Suppliers { get; set; }
    }
}
