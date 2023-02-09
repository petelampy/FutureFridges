using FutureFridges.Business.Email;
using FutureFridges.Business.StockManagement;
using System.Net.Mail;

namespace FutureFridges.Business.HealthReport
{
    public class HealthReportController : IHealthReportController
    {
        private const string SUPPLIER_ORDER_EMAIL_SUBJECT = "Future Fridges - Health Report Extract";
        private const string SUPPLIER_ORDER_EMAIL_BODY = "Your health report export is attached.";
        private const string HEALTH_REPORT_PATH = "./reports/";
        private const string CSV_STRING_DELIMITER = ",";
        private const string CSV_STARTING_LINE = "ProductId,ProductName,ExpiryDate,TimePastExpiry";

        private readonly IStockItemController __StockItemController;
        private readonly IProductController __ProductController;
        private readonly IEmailManager __EmailManager;

        public HealthReportController ()
            : this(new StockItemController(), new ProductController(), new EmailManager())
        {
        }

        internal HealthReportController (IStockItemController stockItemController, IProductController productController, IEmailManager emailManager)
        {
            __StockItemController = stockItemController;
            __ProductController = productController;
            __EmailManager = emailManager;
        }

        public void CreateHealthReport (string safetyOfficerEmail, DateTime date)
        {
            if (!Directory.Exists(HEALTH_REPORT_PATH))
            {
                Directory.CreateDirectory(HEALTH_REPORT_PATH);
            }

            string _Filename = HEALTH_REPORT_PATH + "HealthReport" + "-" + date.Day + "-" + date.Month + "-" + date.Year + ".csv";

            List<StockItem> _StockItems = __StockItemController.GetAll();

            string[] _Data = GenerateCsvString(_StockItems
                .Where(stockItem => stockItem.ExpiryDate < date)
                .ToList(), date);

            File.WriteAllLines(_Filename, _Data);

            SendHealthReportEmail(safetyOfficerEmail, _Filename);
        }

        private void SendHealthReportEmail (string safetyOfficerEmail, string filename)
        {
            Attachment _Pdf = new Attachment(filename);

            __EmailManager.SendEmail(new EmailData
            {
                Recipient = safetyOfficerEmail,
                Subject = SUPPLIER_ORDER_EMAIL_SUBJECT,
                Body = SUPPLIER_ORDER_EMAIL_BODY
            }, _Pdf);
        }

        private string[] GenerateCsvString (List<StockItem> stockItems, DateTime date)
        {
            List<string> _Csv = new List<string>
            {
                CSV_STARTING_LINE
            };

            foreach (StockItem _Item in stockItems)
            {
                Product _Product = __ProductController.GetProduct(_Item.Product_UID);
                TimeSpan _TimeSinceExpiry = date.Subtract(_Item.ExpiryDate);
                
                _Csv.Add(
                    _Item.Item_UID.ToString() + CSV_STRING_DELIMITER
                    + _Product.Name + CSV_STRING_DELIMITER
                    + _Item.ExpiryDate.ToString() + CSV_STRING_DELIMITER
                    + _TimeSinceExpiry.ToString()
                   );
            }
            return _Csv.ToArray();
        }
    }
}