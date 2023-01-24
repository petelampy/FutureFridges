using FutureFridges.Business.OrderManagement;
using FutureFridges.Business.StockManagement;
using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace FutureFridges.Pages.SupplierManagement
{
    public class CreateEditSupplierModel : PageModel
    {
        private readonly ISupplierController __SupplierController;

        public CreateEditSupplierModel()
        {
            __SupplierController = new SupplierController();
        }

        public IActionResult OnGet ()
        {
            if (UID != Guid.Empty)
            {
                Supplier = __SupplierController.Get(UID);

            }
            else
            {
                Supplier = new Supplier();
            }

            return Page();
        }

        public IActionResult OnPost ()
        {
            if (UID != Guid.Empty)
            {
                __SupplierController.Update(Supplier);
            }
            else
            {
                __SupplierController.Create(Supplier);
            }

            return RedirectToPage("SupplierManagement");
        }

        [BindProperty]
        public Supplier Supplier { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid UID { get; set; }
    }
}
