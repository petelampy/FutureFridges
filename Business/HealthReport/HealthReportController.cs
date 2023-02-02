using FutureFridges.Business.Email;
using FutureFridges.Business.OrderManagement;
using FutureFridges.Data.OrderManagement;
using PdfSharpCore;
using PdfSharpCore.Pdf;

namespace FutureFridges.Business.HealthReport
{
    public class HealthReportController
    {
        private const string SUPPLIER_ORDER_EMAIL_SUBJECT = "Future Fridges - You've received an order!";
        private readonly IEmailManager __EmailManager;
        private readonly ISupplierRepository __SupplierRepository; //REPLACE WITH CONTROLLER, FIND A WAY TO DEAL WITH THEM CALLING EACH OTHER
        
        public void createHealthReport(string safetyOfficerEmail, DateTime date)
        {
            //create a pdf of the health report
            PdfDocument _PdfDocument= new PdfDocument();
            //send health report

            SendHeathReportEmail("lewis@llewin.com", "123.pdf");
        }

        private void SendHeathReportEmail (string safetyOfficerEmail, string filename)
        {

            string _SupplierEmailBody = "Attached is a PDF of the health report.";

            __EmailManager.SendEmail(new EmailData()
            {
                Recipient = safetyOfficerEmail,
                Subject = SUPPLIER_ORDER_EMAIL_SUBJECT,
                Body = _SupplierEmailBody
            });
        }
    }
}
