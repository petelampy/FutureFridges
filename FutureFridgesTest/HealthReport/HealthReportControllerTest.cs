using FutureFridges.Business.Email;
using FutureFridges.Business.HealthReport;
using FutureFridges.Business.StockManagement;
using System.Net.Mail;

namespace FutureFridgesTest.HealthReport
{
    [TestClass]
    public class HealthReportControllerTest
    {
        [TestMethod]
        public void HealthReportController_CreateHealthReport_SendsCorrectEmail ()
        {
            Mock<IStockItemController> _MockStockItemController = new Mock<IStockItemController>();
            Mock<IProductController> _MockProductController = new Mock<IProductController>();
            Mock<IEmailManager> _MockEmailManager = new Mock<IEmailManager>();

            Guid _ProductUID = Guid.NewGuid();

            List<StockItem> _StockItems = new List<StockItem>
            {
                new StockItem
                {
                    ExpiryDate = DateTime.Now,
                    Product_UID = _ProductUID,
                    Item_UID = Guid.NewGuid(),
                    Id = 2
                }
            };

            _MockStockItemController.Setup(mock => mock.GetAll()).Returns(_StockItems);

            Product _MockProduct = new Product
            {
                Name = "Test Product",
                UID = _ProductUID,
                Id = 1
            };

            _MockProductController.Setup(mock => mock.GetProduct(_ProductUID)).Returns(_MockProduct);

            EmailData _Result = new EmailData();
            Attachment _ResultAttachment = null;

            _MockEmailManager
                .Setup(mock => mock.SendEmail(It.IsAny<EmailData>(), It.IsAny<Attachment>()))
                .Callback((EmailData email, Attachment attachment) => { _Result = email; _ResultAttachment = attachment; })
                .Verifiable();


            IHealthReportController _HealthReportController = new HealthReportController(_MockStockItemController.Object, _MockProductController.Object, _MockEmailManager.Object);

            string _HealthOfficerEmail = "healthofficer@test.com";

            _HealthReportController.CreateHealthReport(_HealthOfficerEmail, DateTime.MaxValue);

            Assert.AreEqual(_HealthOfficerEmail, _Result.Recipient);
            Assert.AreEqual("Future Fridges - Health Report Extract", _Result.Subject);
            Assert.AreEqual("Your health report export is attached.", _Result.Body);

            Assert.AreEqual("HealthReport" + "-" + DateTime.MaxValue.Day + "-" + DateTime.MaxValue.Month + "-" + DateTime.MaxValue.Year + ".csv", _ResultAttachment.Name);


            StreamReader _StreamReader = new StreamReader(_ResultAttachment.ContentStream);
            char[] _LineCharacters = { '\n', '\r' };

            string _ResultBodyData = _StreamReader
                .ReadToEnd()
                .Trim(_LineCharacters);

            Assert.AreEqual(CreateExpectedFileContent(_StockItems[0], _MockProduct.Name), _ResultBodyData);
            
            _MockProductController.Verify(mock => mock.GetProduct(_ProductUID), Times.Once());
            _MockStockItemController.Verify(mock => mock.GetAll(), Times.Once());
            _MockEmailManager.Verify(mock => mock.SendEmail(It.IsAny<EmailData>(), It.IsAny<Attachment>()), Times.Once());
        }

        private string CreateExpectedFileContent(StockItem stockItem, string productName)
        {
            List<string> _Result = new List<string>();

            _Result.Add("ProductId,ProductName,ExpiryDate,TimePastExpiry\r\n");

            TimeSpan _TimeSinceExpiry = DateTime.MaxValue.Subtract(stockItem.ExpiryDate);

            _Result.Add(
                    stockItem.Item_UID.ToString() + ","
                    + productName + ","
                    + stockItem.ExpiryDate.ToString() + ","
                    + _TimeSinceExpiry.ToString()
                   );

            return string.Join("",  _Result);
        }
    }
}
