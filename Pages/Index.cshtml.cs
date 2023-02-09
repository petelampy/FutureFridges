using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutureFridges.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> __Logger;

        public IndexModel (ILogger<IndexModel> logger)
        {
            __Logger = logger;
        }

        public IActionResult OnGet ()
        {
            return Page();
        }
    }
}