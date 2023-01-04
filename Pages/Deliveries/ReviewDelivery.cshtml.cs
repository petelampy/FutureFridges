using FutureFridges.Business.OrderManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutureFridges.Pages.Deliveries
{
    public class ReviewDeliveryModel : PageModel
    {
        private readonly IOrderController __OrderController;

        public ReviewDeliveryModel ()
        {
            __OrderController = new OrderController();
        }

        public void OnGet ()
        {
            Order = __OrderController.GetOrderByPinCode(PinCode);
        }


        [BindProperty(SupportsGet = true)]
        public Order Order { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PinCode { get; set; }
    }
}
