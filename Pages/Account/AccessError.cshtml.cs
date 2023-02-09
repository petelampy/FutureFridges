using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutureFridges.Pages.Account
{
    public class AccessErrorModel : PageModel
    {
        public IActionResult OnGet ()
        {
            return Page();
        }
    }
}
