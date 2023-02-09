using FutureFridges.Business.Enums;
using FutureFridges.Business.OrderManagement;
using FutureFridges.Business.StockManagement;
using FutureFridges.Data.StockManagement;

namespace FutureFridgesTest.StockManagement
{
    [TestClass]
    public class ProductControllerTest
    {
        [TestMethod]
        public void ProductController_Create_GeneratesValidProduct ()
        {
            Mock<IProductRepository> _MockRepository = new Mock<IProductRepository>();
            Mock<IStockItemController> _MockStockItemController = new Mock<IStockItemController>();
            Mock<IOrderController> _MockOrderController = new Mock<IOrderController>();

            Product _Result = new Product();

            _MockRepository.Setup(mock => mock.CreateProduct(It.IsAny<Product>())).Callback((Product product) =>
            {
                _Result = product;
            }).Verifiable();

            IProductController _ProductController = new ProductController(_MockRepository.Object, _MockStockItemController.Object, _MockOrderController.Object);

            Product _Product = new Product
            {
                Category = ProductCategory.Dairy,
                DaysShelfLife = 10,
                ImageName = "test.jpg",
                Name = "Cheese",
                MinimumStockLevel = 4,
                Supplier_UID = Guid.NewGuid(),
                Id = 15
            };

            _ProductController.CreateProduct(_Product);
            
            _MockRepository.Verify(repo => repo.CreateProduct(It.IsAny<Product>()), Times.Once());

            Assert.AreNotEqual(Guid.Empty, _Result.UID);

            Assert.AreEqual(_Product.Category, _Result.Category);
            Assert.AreEqual(_Product.DaysShelfLife, _Result.DaysShelfLife);
            Assert.AreEqual(_Product.ImageName, _Result.ImageName);
            Assert.AreEqual(_Product.Name, _Result.Name);
            Assert.AreEqual(_Product.MinimumStockLevel, _Result.MinimumStockLevel);
            Assert.AreEqual(_Product.Supplier_UID, _Result.Supplier_UID);
            Assert.AreEqual(_Product.Id, _Result.Id);
        }

        [TestMethod]
        public void ProductController_IsProductInUse_ReturnsTrueWhenInUse ()
        {
            Mock<IProductRepository> _MockRepository = new Mock<IProductRepository>();
            Mock<IStockItemController> _MockStockItemController = new Mock<IStockItemController>();
            Mock<IOrderController> _MockOrderController = new Mock<IOrderController>();

            _MockStockItemController
                .Setup(mock => mock.GetStockItemsByProduct(It.IsAny<Guid>()))
                .Returns(new List<StockItem> { new StockItem(), new StockItem() })
                .Verifiable();

            _MockOrderController
                .Setup(mock => mock.GetOrderItemsByProduct(It.IsAny<Guid>()))
                .Returns(new List<OrderItem> { new OrderItem(), new OrderItem() })
                .Verifiable();

            IProductController _ProductController = new ProductController(_MockRepository.Object, _MockStockItemController.Object, _MockOrderController.Object);
            bool _Result = _ProductController.IsProductInUse(Guid.NewGuid());

            _MockStockItemController.Verify(repo => repo.GetStockItemsByProduct(It.IsAny<Guid>()), Times.Once());
            _MockOrderController.Verify(repo => repo.GetOrderItemsByProduct(It.IsAny<Guid>()), Times.Once());

            Assert.AreEqual(true, _Result);
        }

        [TestMethod]
        public void ProductController_IsProductInUse_ReturnsFalseWhenNotInUse ()
        {
            Mock<IProductRepository> _MockRepository = new Mock<IProductRepository>();
            Mock<IStockItemController> _MockStockItemController = new Mock<IStockItemController>();
            Mock<IOrderController> _MockOrderController = new Mock<IOrderController>();

            _MockStockItemController
                .Setup(mock => mock.GetStockItemsByProduct(It.IsAny<Guid>()))
                .Returns(new List<StockItem>())
                .Verifiable();

            _MockOrderController
                .Setup(mock => mock.GetOrderItemsByProduct(It.IsAny<Guid>()))
                .Returns(new List<OrderItem>())
                .Verifiable();

            IProductController _ProductController = new ProductController(_MockRepository.Object, _MockStockItemController.Object, _MockOrderController.Object);
            bool _Result = _ProductController.IsProductInUse(Guid.NewGuid());

            _MockStockItemController.Verify(repo => repo.GetStockItemsByProduct(It.IsAny<Guid>()), Times.Once());
            _MockOrderController.Verify(repo => repo.GetOrderItemsByProduct(It.IsAny<Guid>()), Times.Once());

            Assert.AreEqual(false, _Result);
        }
    }
}