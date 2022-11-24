using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutureFridges.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> __Logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            __Logger = logger;
        }

        public void OnGet()
        {
        }
    }
}