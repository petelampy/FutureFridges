using FutureFridges.Business.Email;
using FutureFridges.Business.OrderManagement;
using FutureFridges.Business.StockManagement;
using FutureFridges.Data.OrderManagement;
using FutureFridges.Data.StockManagement;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureFridgesTest.OrderManagement
{
    [TestClass]
    public class OrderControllerTest
    {
        [TestMethod]
        public void OrderController_Create_GeneratesValidOrder()
        {
            Mock<IOrderRepository> _MockRepository = new Mock<IOrderRepository>();
            Mock<IProductRepository> _MockProductRepository = new Mock<IProductRepository>();
            Mock<ISupplierRepository> _MockSupplierRepository = new Mock<ISupplierRepository>();
            Mock<IEmailManager> _MockEmailManager = new Mock<IEmailManager>();

            Order _Result = new Order();

            Order _Order = new Order
            {
                Id = 22,
                NumberOfItems = 15,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Id = 5,
                        ProductName = "Test",
                        Product_UID = Guid.NewGuid(),
                        Quantity = 6,
                        Supplier_UID = Guid.NewGuid(),
                        UID = Guid.NewGuid(),
                    }
                },
                Supplier_UID = Guid.NewGuid()
            };

            _MockRepository
                .Setup(mock => mock.CreateOrder(It.IsAny<Order>()))
                .Callback((Order order) => { _Result = order; })
                .Verifiable();

            _MockRepository
                .Setup(mock => mock.GetAll())
                .Returns(new List<Order> { _Order });

            IOrderController _OrderController = new OrderController(_MockRepository.Object, _MockProductRepository.Object, _MockSupplierRepository.Object, _MockEmailManager.Object);
            _OrderController.CreateOrder(_Order);

            _MockRepository.Verify(repo => repo.CreateOrder(It.IsAny<Order>()), Times.Once());
            _MockRepository.Verify(repo => repo.GetAll(), Times.Once());

            Assert.AreNotEqual(Guid.Empty, _Result.UID);

            Assert.IsTrue(_Result.PinCode > 0);
            Assert.IsTrue(_Result.PinCode < 9999);

            Assert.AreEqual(_Order.Id, _Result.Id);
            Assert.AreEqual(_Order.Supplier_UID, _Result.Supplier_UID);
            Assert.AreEqual(_Order.OrderItems.Select(orderItem => orderItem.Quantity).Sum(), _Result.NumberOfItems);
            
            Assert.AreEqual(_Order.OrderItems[0].Id, _Result.OrderItems[0].Id);
            Assert.AreEqual(_Order.OrderItems[0].ProductName, _Result.OrderItems[0].ProductName);
            Assert.AreEqual(_Order.OrderItems[0].Product_UID, _Result.OrderItems[0].Product_UID);
            Assert.AreEqual(_Order.OrderItems[0].Quantity, _Result.OrderItems[0].Quantity);
        }

        [TestMethod]
        public void OrderController_CreateOrderItem_GeneratesValidOrderItem ()
        {
            Mock<IOrderRepository> _MockRepository = new Mock<IOrderRepository>();
            Mock<IProductRepository> _MockProductRepository = new Mock<IProductRepository>();
            Mock<ISupplierRepository> _MockSupplierRepository = new Mock<ISupplierRepository>();
            Mock<IEmailManager> _MockEmailManager = new Mock<IEmailManager>();

            OrderItem _Result = new OrderItem();

            OrderItem _OrderItem = new OrderItem
            {
                Id = 7,
                ProductName = "Cheez",
                Product_UID = Guid.NewGuid(),
                Quantity = 4,
                Supplier_UID = Guid.NewGuid()
            };

            _MockRepository
                .Setup(mock => mock.CreateOrderItem(It.IsAny<OrderItem>()))
                .Callback((OrderItem orderItem) => { _Result = orderItem; })
                .Verifiable();

            IOrderController _OrderController = new OrderController(_MockRepository.Object, _MockProductRepository.Object, _MockSupplierRepository.Object, _MockEmailManager.Object);
            _OrderController.CreateOrderItem(_OrderItem);

            _MockRepository.Verify(repo => repo.CreateOrderItem(It.IsAny<OrderItem>()), Times.Once());

            Assert.AreNotEqual(Guid.Empty, _Result.UID);

            Assert.AreEqual(_OrderItem.Id, _Result.Id);
            Assert.AreEqual(_OrderItem.ProductName, _Result.ProductName);
            Assert.AreEqual(_OrderItem.Product_UID, _Result.Product_UID);
            Assert.AreEqual(_OrderItem.Supplier_UID, _Result.Supplier_UID);
            Assert.AreEqual(_OrderItem.Quantity, _Result.Quantity);
        }

        [TestMethod]
        public void OrderController_GetOrder_FetchesCorrectProductNames ()
        {
            Mock<IOrderRepository> _MockRepository = new Mock<IOrderRepository>();
            Mock<IProductRepository> _MockProductRepository = new Mock<IProductRepository>();
            Mock<ISupplierRepository> _MockSupplierRepository = new Mock<ISupplierRepository>();
            Mock<IEmailManager> _MockEmailManager = new Mock<IEmailManager>();

            Order _Order = new Order();
            _Order.OrderItems = new List<OrderItem>();
            OrderItem _OrderItem = new OrderItem
            {
                Id = 7,
                ProductName = "",
                Product_UID = Guid.NewGuid(),
                Quantity = 4,
                Supplier_UID = Guid.NewGuid()
            };
            OrderItem _OrderItem2 = new OrderItem
            {
                Id = 4,
                ProductName = "",
                Product_UID = Guid.NewGuid(),
                Quantity = 2,
                Supplier_UID = Guid.NewGuid()
            };
            _Order.OrderItems.Add(_OrderItem);
            _Order.OrderItems.Add(_OrderItem2);

            _MockRepository
                .Setup(mock => mock.GetOrder(It.IsAny<Guid>()))
                .Returns(_Order);

            Product _Product = new Product
            {
                Name = "Test Product Name"
            };

            _MockProductRepository
                .Setup(mock => mock.GetProduct(It.IsAny<Guid>()))
                .Returns(_Product);

            IOrderController _OrderController = new OrderController(_MockRepository.Object, _MockProductRepository.Object, _MockSupplierRepository.Object, _MockEmailManager.Object);
            Order _Result = _OrderController.GetOrder(Guid.Empty);

            _MockRepository.Verify(repo => repo.GetOrder(It.IsAny<Guid>()), Times.Once());
            _MockProductRepository.Verify(repo => repo.GetProduct(It.IsAny<Guid>()), Times.Exactly(2));

            Assert.AreEqual(_Product.Name, _Result.OrderItems[0].ProductName);
            Assert.AreEqual(_Product.Name, _Result.OrderItems[1].ProductName);
        }

        [TestMethod]
        public void OrderController_GetOrderByPinCode_FetchesCorrectProductNames ()
        {
            Mock<IOrderRepository> _MockRepository = new Mock<IOrderRepository>();
            Mock<IProductRepository> _MockProductRepository = new Mock<IProductRepository>();
            Mock<ISupplierRepository> _MockSupplierRepository = new Mock<ISupplierRepository>();
            Mock<IEmailManager> _MockEmailManager = new Mock<IEmailManager>();

            Order _Order = new Order();
            _Order.OrderItems = new List<OrderItem>();
            OrderItem _OrderItem = new OrderItem
            {
                Id = 7,
                ProductName = "",
                Product_UID = Guid.NewGuid(),
                Quantity = 4,
                Supplier_UID = Guid.NewGuid()
            };
            OrderItem _OrderItem2 = new OrderItem
            {
                Id = 4,
                ProductName = "",
                Product_UID = Guid.NewGuid(),
                Quantity = 2,
                Supplier_UID = Guid.NewGuid()
            };
            _Order.OrderItems.Add(_OrderItem);
            _Order.OrderItems.Add(_OrderItem2);

            _MockRepository
                .Setup(mock => mock.GetOrderByPinCode(It.IsAny<int>()))
                .Returns(_Order);

            Product _Product = new Product
            {
                Name = "Test Product Name"
            };

            _MockProductRepository
                .Setup(mock => mock.GetProduct(It.IsAny<Guid>()))
                .Returns(_Product);

            IOrderController _OrderController = new OrderController(_MockRepository.Object, _MockProductRepository.Object, _MockSupplierRepository.Object, _MockEmailManager.Object);
            Order _Result = _OrderController.GetOrderByPinCode(0);

            _MockRepository.Verify(repo => repo.GetOrderByPinCode(It.IsAny<int>()), Times.Once());
            _MockProductRepository.Verify(repo => repo.GetProduct(It.IsAny<Guid>()), Times.Exactly(2));

            Assert.AreEqual(_Product.Name, _Result.OrderItems[0].ProductName);
            Assert.AreEqual(_Product.Name, _Result.OrderItems[1].ProductName);
        }

        [TestMethod]
        public void OrderController_CompleteOrder_SendsCorrectEmail ()
        {
            Mock<IOrderRepository> _MockRepository = new Mock<IOrderRepository>();
            Mock<IProductRepository> _MockProductRepository = new Mock<IProductRepository>();
            Mock<ISupplierRepository> _MockSupplierRepository = new Mock<ISupplierRepository>();
            Mock<IEmailManager> _MockEmailManager = new Mock<IEmailManager>();

            Guid _MockSupplierGuid = Guid.NewGuid();

            Order _Order = new Order();
            _Order.OrderItems = new List<OrderItem>();
            OrderItem _OrderItem = new OrderItem
            {
                Id = 3,
                Product_UID = Guid.NewGuid(),
                Quantity = 4,
                Supplier_UID = _MockSupplierGuid
            };
            OrderItem _OrderItem2 = new OrderItem
            {
                Id = 1,
                Product_UID = Guid.NewGuid(),
                Quantity = 6,
                Supplier_UID = _MockSupplierGuid
            };
            _Order.OrderItems.Add(_OrderItem);
            _Order.OrderItems.Add(_OrderItem2);

            Supplier _Supplier = new Supplier
            {
                Name = "Test Supplier",
                Email = "testsupplier@email.com"
            };

            _MockRepository
                .Setup(mock => mock.GetOrder(It.IsAny<Guid>()))
                .Returns(_Order);

            _MockSupplierRepository
                .Setup(mock => mock.Get(It.IsAny<Guid>()))
                .Returns(_Supplier);

            EmailData _Result = new EmailData();

            _MockEmailManager
                .Setup(mock => mock.SendEmail(It.IsAny<EmailData>()))
                .Callback((EmailData emailData) => { _Result = emailData; })
                .Verifiable();

            Order _ResultOrder = new Order();

            _MockRepository.Setup(mock => mock.CreateOrder(It.IsAny<Order>()))
                .Callback((Order resultOrder) => { _ResultOrder = resultOrder; });

            _MockRepository.Setup(mock => mock.GetAll()).Returns(new List<Order> { new Order
                {
                    PinCode = 11
                } 
            });

            _MockRepository.Setup(mock => mock.CreateOrderItem(It.IsAny<OrderItem>()));

            _MockProductRepository.Setup(mock => mock.GetProduct(It.IsAny<Guid>())).Returns(new Product
            {
                Name = "Beans"
            });

            IOrderController _OrderController = new OrderController(_MockRepository.Object, _MockProductRepository.Object, _MockSupplierRepository.Object, _MockEmailManager.Object);
            _OrderController.CompleteOrder(Guid.Empty);

            _MockSupplierRepository.Verify(repo => repo.Get(It.IsAny<Guid>()), Times.Once);
            _MockEmailManager.Verify(manager => manager.SendEmail(It.IsAny<EmailData>()), Times.Once);
            _MockRepository.Verify(repo => repo.CreateOrder(It.IsAny<Order>()), Times.Once);
            _MockRepository.Verify(repo => repo.CreateOrderItem(It.IsAny<OrderItem>()), Times.Exactly(2));

            Assert.AreEqual(_Supplier.Email, _Result.Recipient);
            Assert.AreEqual("Future Fridges - You've received an order!", _Result.Subject);
            Assert.AreEqual(CreateExpectedEmailBody(_Supplier, _Order.OrderItems, _ResultOrder.PinCode), _Result.Body);
        }

        private string CreateExpectedEmailBody(Supplier supplier, List<OrderItem> orderItems, int pinCode)
        {
            StringBuilder _StringBuilder = new StringBuilder();

           _StringBuilder.Append("Hello, " + supplier.Name + "!\n\nYou have received the following order:\n\n");

            foreach(OrderItem _Item in orderItems)
            {
                _StringBuilder.Append(_Item.Quantity + " x " + _Item.ProductName + "\n");
            }

            _StringBuilder.Append("\nPlease use the following pin code when completing delivery: " + pinCode + "\n\nKind Regards, Future Fridges!");

            return _StringBuilder.ToString();
        }
    }
}
