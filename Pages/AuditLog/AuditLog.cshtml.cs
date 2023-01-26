using FutureFridges.Business.AuditLog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutureFridges.Pages.Logs
{
    public class AuditLogModel : PageModel
    {

        private readonly IAuditLogController __AuditLogController;

        public AuditLogModel ()
        {
            __AuditLogController = new AuditLogController();
        }

        public IActionResult OnGet ()
        {
            //Get current user account, if admin show all logs, if head chef show stock logs
            //Logs = __AuditLogController.GetStockLogs();
            Logs = __AuditLogController.GetAll();

            return Page();
        }

        public List<LogEntry> Logs { get; set; }
    }
}
