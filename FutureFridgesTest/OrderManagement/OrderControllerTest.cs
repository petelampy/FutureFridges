using FutureFridges.Business.Email;
using FutureFridges.Business.OrderManagement;
using FutureFridges.Data.OrderManagement;
using FutureFridges.Data.StockManagement;
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

            Order _TestOrder = new Order()
            {
                Id = 22,
                NumberOfItems = 15,
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem()
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
                .Returns(new List<Order>() { _TestOrder });

            IOrderController _OrderController = new OrderController(_MockRepository.Object, _MockProductRepository.Object, _MockSupplierRepository.Object, _MockEmailManager.Object);
            _OrderController.CreateOrder(_TestOrder);

            _MockRepository.Verify(repo => repo.CreateOrder(It.IsAny<Order>()), Times.Once());
            _MockRepository.Verify(repo => repo.GetAll(), Times.Once());

            Assert.AreNotEqual(Guid.Empty, _Result.UID);
            Assert.IsTrue(_Result.PinCode > 0);

            Assert.AreEqual(_TestOrder.Id, _Result.Id);
            Assert.AreEqual(_TestOrder.Supplier_UID, _Result.Supplier_UID);
            Assert.AreEqual(_TestOrder.OrderItems.Select(orderItem => orderItem.Quantity).Sum(), _Result.NumberOfItems);
            
            Assert.AreEqual(_TestOrder.OrderItems[0].Id, _Result.OrderItems[0].Id);
            Assert.AreEqual(_TestOrder.OrderItems[0].ProductName, _Result.OrderItems[0].ProductName);
            Assert.AreEqual(_TestOrder.OrderItems[0].Product_UID, _Result.OrderItems[0].Product_UID);
            Assert.AreEqual(_TestOrder.OrderItems[0].Quantity, _Result.OrderItems[0].Quantity);
        }

        //ADD TESTS FOR OTHER STUFF LIKE PIN CODE, ANYTHING THAT HAS LOGIC
    }
}
