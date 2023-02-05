using FutureFridges.Business.Email;
using FutureFridges.Business.StockManagement;
using System.Net.Mail;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace FutureFridges.Business.HealthReport
{
    public class HealthReportController
    {
        private const string SUPPLIER_ORDER_EMAIL_SUBJECT = "Future Fridges - Health Report Extract";
        private const string HEALTH_REPORT_PATH = "./reports/";
        
        public void createHealthReport(string safetyOfficerEmail, DateTime date)
        {
            if (!System.IO.Directory.Exists(HEALTH_REPORT_PATH))
            {
                System.IO.Directory.CreateDirectory(HEALTH_REPORT_PATH);
            }
            //create a pdf of the health report
            string _Filename = HEALTH_REPORT_PATH + "HealthReport" + "-" + date.Day + "-" + date.Month + ".pdf";

            List<StockItem> _StockItems= new List<StockItem>();
            StockItemController _StockItemCtr = new StockItemController();
            _StockItems = _StockItemCtr.GetAll();
            List<StockItem> _ExpiredStockItems = new List<StockItem>();
            //for each stockitem if it has expired add it to a table containing html elements to put into the pdf
            foreach (StockItem _Item in _StockItems )
            {
                if (_Item.ExpiryDate < date)
                {
                    _ExpiredStockItems.Add(_Item);
                }
            }
            
            string _HTMLString = GetHTMLString(_ExpiredStockItems, date);

            var _Pdf = PdfGenerator.GeneratePdf(_HTMLString, PdfSharp.PageSize.Letter);

            _Pdf.Save(_Filename);

            SendHeathReportEmail("lewis@llewin.com", _Filename);
        }

        private void SendHeathReportEmail (string safetyOfficerEmail, string filename)
        {
            string _SupplierEmailBody = "Your health report export is attached.";
            EmailManager _EmailManager = new EmailManager();
            Attachment _Pdf = new Attachment(filename);
            _EmailManager.SendEmail(new EmailData()
            {
                Recipient = safetyOfficerEmail,
                Subject = SUPPLIER_ORDER_EMAIL_SUBJECT,
                Body = _SupplierEmailBody
            }, _Pdf);
        }

        private string GetHTMLString(List<StockItem> stockItems, DateTime date)
        {
            string _HTMLString = "<h1>Health Report<h2>";
            string _Table = " <table><thead><tr><th>    Product ID</th><th>    Product Name</th><th>    Expiry Date</th><th>    Time Past Expiry</th></tr></thead><tbody>";
            //add each line of the tbody
            /*foreach (StockItem stockItem in stockItems)
            {
                //get expiry date from db

                //if past expiry display with time past expiry
                TimeSpan ExpiryDiff = DateTime.Now.Subtract(stockItem.ExpiryDate);
                if(stockItem.ExpiryDate < DateTime.Now)
                            {
                    Product _Product = Model.Products.Where(product => product.UID == stockItem.Product_UID).First();
                                < tr >
                                    < td > @stockItem.Item_UID </ td >
                                    < td > @_Product.Name </ td >
                                    < td > @stockItem.ExpiryDate </ td >
                                    < td > @ExpiryDiff.Days Day(s) @ExpiryDiff.Hours Hour (s) @ExpiryDiff.Minutes Minute (s)</ td >
                                </ tr >
                            }
            }*/
            foreach (StockItem _Item in stockItems)
            {
                TimeSpan _ExpiryDiff = date.Subtract(_Item.ExpiryDate);
                ProductController _ProductController = new ProductController();
                
                //add tr
                _Table = _Table + "<tr>";
                //add each td and the data
                _Table = _Table + "<td>" + _Item.Item_UID + "</td>";
                _Table = _Table + "<td>" + _ProductController.GetProduct(_Item.Product_UID).Name + "</td>";
                _Table = _Table + "<td>" + _Item.ExpiryDate + "</td>";
                _Table = _Table + "<td>" + _ExpiryDiff.Days + "Day(s)" + _ExpiryDiff.Hours + "Hour(s)" + _ExpiryDiff.Minutes + "Minute(s)" + "</td>";
                //close tr
                _Table = _Table + "</tr>";
            }
            //close off table
            _Table = _Table + "</tbody></table>";
            _HTMLString = _HTMLString + _Table;
            //add to HTML string
            return _HTMLString;
        }
    }
}
