using FutureFridges.Business.Email;
using FutureFridges.Business.StockManagement;
using System.Net.Mail;

namespace FutureFridges.Business.HealthReport
{
    public class HealthReportController
    {
        private const string SUPPLIER_ORDER_EMAIL_SUBJECT = "Future Fridges - Health Report Extract";
        private const string HEALTH_REPORT_PATH = "./reports/";

        public void createHealthReport (string safetyOfficerEmail, DateTime date)
        {
            if (!System.IO.Directory.Exists(HEALTH_REPORT_PATH))
            {
                System.IO.Directory.CreateDirectory(HEALTH_REPORT_PATH);
            }

            string _Filename = HEALTH_REPORT_PATH + "HealthReport" + "-" + date.Day + "-" + date.Month + "-" + date.Year + ".csv";

            List<StockItem> _StockItems = new StockItemController().GetAll();

            string[] _Data = GenerateCsvString(GetExpiredStockItems(_StockItems, date));

            File.WriteAllLines(_Filename, _Data);

            SendHealthReportEmail(safetyOfficerEmail, _Filename);
        }

        private void SendHealthReportEmail (string safetyOfficerEmail, string filename)
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

        public List<StockItem> GetExpiredStockItems(List<StockItem> stockItems, DateTime date)
        {
            List<StockItem> _ExpiredStockItems = new List<StockItem>();

            foreach (StockItem _Item in stockItems)
            {
                if (_Item.ExpiryDate < date)
                {
                    _ExpiredStockItems.Add(_Item);
                }
            }

            return _ExpiredStockItems;
        }

        private string[] GenerateCsvString(List<StockItem> stockItems)
        {
            const string DELIM = ",";
            List<string> _Csv = new List<string>
            {
                "ProductId,ProductName,ExpiryDate,TimePastExpiry"
            };

            ProductController _ProductController = new ProductController();

            foreach (StockItem _Item in stockItems)
            {
                Product _Product = _ProductController.GetProduct(_Item.Product_UID);
                TimeSpan _TimeSinceExpiry = DateTime.Now.Subtract(_Item.ExpiryDate);
                _Csv.Add(
                    _Item.Item_UID.ToString() + DELIM
                    + _Product.Name + DELIM
                    + _Item.ExpiryDate.ToString() + DELIM
                    + _TimeSinceExpiry.ToString()
                   );
            }
            return _Csv.ToArray();
        }
    }
}