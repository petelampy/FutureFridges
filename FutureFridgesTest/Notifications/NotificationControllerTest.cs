using FutureFridges.Business.Admin;
using FutureFridges.Business.Enums;
using FutureFridges.Business.Notifications;
using FutureFridges.Business.OrderManagement;
using FutureFridges.Business.StockManagement;
using FutureFridges.Business.UserManagement;
using FutureFridges.Data.Notifications;

namespace FutureFridgesTest.Notifications
{
    [TestClass]
    public class NotificationControllerTest
    {
        [TestMethod]
        public void NotificationController_CreateProductNotification_GeneratesNotificationIfLowStock ()
        {
            Mock<INotificationRepository> _MockRepository = new Mock<INotificationRepository>();
            Mock<IOrderController> _MockOrderController = new Mock<IOrderController>();
            Mock<IProductController> _MockProductController = new Mock<IProductController>();
            Mock<ISettingsController> _MockSettingsController = new Mock<ISettingsController>();
            Mock<IStockItemController> _MockStockItemController = new Mock<IStockItemController>();
            Mock<IUserController> _MockUserController = new Mock<IUserController>();

            Guid _MockProductGuid = Guid.NewGuid();
            Guid _MockNotifiedUserUID = Guid.NewGuid();
            
            Product _MockProduct = new Product()
            {
                UID = _MockProductGuid,
                Category = ProductCategory.Dairy,
                DaysShelfLife = 4,
                MinimumStockLevel = 2,
                Name = "Cheese",
                Supplier_UID = Guid.NewGuid()
            };

            _MockProductController.Setup(mock => mock.GetProduct(_MockProductGuid)).Returns(_MockProduct);
            _MockStockItemController.Setup(mock => mock.GetAll()).Returns(new List<StockItem>());
            _MockOrderController.Setup(mock => mock.GetAll()).Returns(new List<Order>());

            _MockSettingsController.Setup(mock => mock.Get()).Returns(new Settings()
            {
                Administrator_UID = _MockNotifiedUserUID,
                NotifyAllHeadChefs = false
            });

            _MockRepository.Setup(mock => mock.GetAll()).Returns(new List<Notification>());

            Notification _Result = new Notification();

            _MockRepository
                .Setup(mock => mock.Create(It.IsAny<Notification>()))
                .Callback((Notification notification) => { _Result = notification; })
                .Verifiable();
            
            INotificationController _NotificationController = new NotificationController(_MockRepository.Object, _MockStockItemController.Object,
                _MockProductController.Object, _MockSettingsController.Object, _MockOrderController.Object, _MockUserController.Object);
            
            _NotificationController.CreateProductNotification(_MockProductGuid);

            Assert.AreNotEqual(Guid.Empty, _Result.UID);

            Assert.AreEqual(_MockProductGuid, _Result.Product_UID);
            Assert.AreEqual(_MockNotifiedUserUID, _Result.User_UID);
            Assert.AreEqual("There is low stock of Cheese. An order needs placing", _Result.Message);
            Assert.IsTrue(_Result.DateCreated > DateTime.MinValue);

            _MockRepository.Verify(mock => mock.Create(It.IsAny<Notification>()), Times.Once);
            _MockRepository.Verify(mock => mock.GetAll(), Times.Once);
            _MockProductController.Verify(mock => mock.GetProduct(_MockProductGuid), Times.Once);
            _MockStockItemController.Verify(mock => mock.GetAll(), Times.Once);
            _MockOrderController.Verify(mock => mock.GetAll(), Times.Once);
            _MockSettingsController.Verify(mock => mock.Get(), Times.Once);
        }

        [TestMethod]
        public void NotificationController_CreateProductNotification_DoesNotGenerateNotificationIfStockAboveMinimum ()
        {
            Mock<INotificationRepository> _MockRepository = new Mock<INotificationRepository>();
            Mock<IOrderController> _MockOrderController = new Mock<IOrderController>();
            Mock<IProductController> _MockProductController = new Mock<IProductController>();
            Mock<ISettingsController> _MockSettingsController = new Mock<ISettingsController>();
            Mock<IStockItemController> _MockStockItemController = new Mock<IStockItemController>();
            Mock<IUserController> _MockUserController = new Mock<IUserController>();

            Guid _MockProductGuid = Guid.NewGuid();
            Guid _MockNotifiedUserUID = Guid.NewGuid();

            Product _MockProduct = new Product()
            {
                UID = _MockProductGuid,
                Category = ProductCategory.Dairy,
                DaysShelfLife = 3,
                MinimumStockLevel = 1,
                Name = "Cheese",
                Supplier_UID = Guid.NewGuid()
            };

            List<StockItem> _MockStockItems = new List<StockItem>()
            {
                new StockItem()
                {
                    Product_UID = _MockProductGuid,
                    ExpiryDate = DateTime.MaxValue,
                    Item_UID = Guid.NewGuid()
                },
                new StockItem()
                {
                    Product_UID = _MockProductGuid,
                    ExpiryDate = DateTime.MaxValue,
                    Item_UID = Guid.NewGuid()
                },
            };

            _MockProductController.Setup(mock => mock.GetProduct(_MockProductGuid)).Returns(_MockProduct);
            _MockStockItemController.Setup(mock => mock.GetAll()).Returns(_MockStockItems);
            _MockOrderController.Setup(mock => mock.GetAll()).Returns(new List<Order>());

            _MockSettingsController.Setup(mock => mock.Get()).Returns(new Settings()
            {
                Administrator_UID = _MockNotifiedUserUID,
                NotifyAllHeadChefs = false
            });

            _MockRepository.Setup(mock => mock.GetAll()).Returns(new List<Notification>());

            _MockRepository
                .Setup(mock => mock.Create(It.IsAny<Notification>()))
                .Verifiable();

            INotificationController _NotificationController = new NotificationController(_MockRepository.Object, _MockStockItemController.Object,
                _MockProductController.Object, _MockSettingsController.Object, _MockOrderController.Object, _MockUserController.Object);

            _NotificationController.CreateProductNotification(_MockProductGuid);
            
            _MockRepository.Verify(mock => mock.Create(It.IsAny<Notification>()), Times.Never);

            _MockRepository.Verify(mock => mock.GetAll(), Times.Once);
            _MockProductController.Verify(mock => mock.GetProduct(_MockProductGuid), Times.Once);
            _MockStockItemController.Verify(mock => mock.GetAll(), Times.Once);
            _MockOrderController.Verify(mock => mock.GetAll(), Times.Once);
            _MockSettingsController.Verify(mock => mock.Get(), Times.Once);
        }
    }
}
