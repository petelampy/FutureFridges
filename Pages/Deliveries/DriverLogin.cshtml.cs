using FutureFridges.Business.OrderManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace FutureFridges.Pages.Deliveries
{
    public class DriverLoginModel : PageModel
    {
        private readonly IOrderController __OrderController;

        public DriverLoginModel ()
        {
            __OrderController = new OrderController();
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost ()
        {

            if(!__OrderController.IsValidOrderPinCode(PinCode))
            {
                ModelState.AddModelError("error", "Invalid Pin Code");
                return Page();
            }
            else
            {
                return RedirectToPage("./ReviewDelivery", new { PinCode = PinCode });
            }
        }

        [BindProperty]
        [Required(ErrorMessage = "Please enter your pin code!")]
        public int PinCode { get; set; }
    }
}
