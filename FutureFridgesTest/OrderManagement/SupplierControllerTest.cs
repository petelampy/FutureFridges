using FutureFridges.Business.OrderManagement;
using FutureFridges.Business.StockManagement;
using FutureFridges.Data.OrderManagement;

namespace FutureFridgesTest.OrderManagement
{
    [TestClass]
    public class SupplierControllerTest
    {

        [TestMethod]
        public void SupplierController_Create_GeneratesValidSupplier ()
        {
            Mock<ISupplierRepository> _MockRepository = new Mock<ISupplierRepository>();
            Mock<IOrderController> _MockOrderController = new Mock<IOrderController>();
            Mock<IProductController> _MockProductController = new Mock<IProductController>();

            Supplier _Supplier = new Supplier
            {
                Email = "test@supplier.com",
                Name = "Test Supplier",
                Id = 55
            };

            Supplier _Result = new Supplier();

            _MockRepository
                .Setup(mock => mock.Create(It.IsAny<Supplier>()))
                .Callback((Supplier supplier) => { _Result = supplier; })
                .Verifiable();

            ISupplierController _SupplierController = new SupplierController(_MockRepository.Object, _MockProductController.Object, _MockOrderController.Object);

            _SupplierController.Create(_Supplier);

            Assert.AreNotEqual(Guid.Empty, _Result.UID);

            Assert.AreEqual(_Supplier.Name, _Result.Name);
            Assert.AreEqual(_Supplier.Email, _Result.Email);
            Assert.AreEqual(_Supplier.Id, _Result.Id);

            _MockRepository.Verify(mock => mock.Create(It.IsAny<Supplier>()), Times.Once);
        }

        [TestMethod]
        public void SupplierController_GetByProduct_ReturnsCorrectSupplierUID ()
        {
            Mock<ISupplierRepository> _MockRepository = new Mock<ISupplierRepository>();
            Mock<IOrderController> _MockOrderController = new Mock<IOrderController>();
            Mock<IProductController> _MockProductController = new Mock<IProductController>();

            Product _Product = new Product
            {
                Name = "Product",
                Id = 55,
                Supplier_UID = Guid.NewGuid(),
            };

            _MockProductController
                .Setup(mock => mock.GetProduct(It.IsAny<Guid>()))
                .Returns(_Product);

            _MockRepository
                .Setup(mock => mock.Get(It.IsAny<Guid>()))
                .Returns((Guid supplier_UID) =>
                {
                    return new Supplier
                    {
                        UID = supplier_UID
                    };
                });

            ISupplierController _SupplierController = new SupplierController(_MockRepository.Object, _MockProductController.Object, _MockOrderController.Object);

            Supplier _Result = _SupplierController.GetByProduct(Guid.NewGuid());

            Assert.AreNotEqual(Guid.Empty, _Result.UID);

            Assert.AreEqual(_Product.Supplier_UID, _Result.UID);

            _MockRepository.Verify(mock => mock.Get(It.IsAny<Guid>()), Times.Once);
            _MockProductController.Verify(mock => mock.GetProduct(It.IsAny<Guid>()), Times.Once);
        }

        [TestMethod]
        public void SupplierController_IsEmailInUse_ReturnsFalseIfNotInUse ()
        {
            Mock<ISupplierRepository> _MockRepository = new Mock<ISupplierRepository>();
            Mock<IOrderController> _MockOrderController = new Mock<IOrderController>();
            Mock<IProductController> _MockProductController = new Mock<IProductController>();

            List<Supplier> _Suppliers = new List<Supplier> {
                new Supplier
                {
                    Email = "test225@email.com"
                }
            };

            _MockRepository.Setup(mock => mock.GetAll()).Returns(_Suppliers);

            ISupplierController _SupplierController = new SupplierController(_MockRepository.Object, _MockProductController.Object, _MockOrderController.Object);

            bool _Result = _SupplierController.IsEmailInUse("test333@email.com");

            Assert.AreEqual(false, _Result);

            _MockRepository.Verify(mock => mock.GetAll(), Times.Once);
        }

        [TestMethod]
        public void SupplierController_IsEmailInUse_ReturnsTrueIfInUse ()
        {
            Mock<ISupplierRepository> _MockRepository = new Mock<ISupplierRepository>();
            Mock<IOrderController> _MockOrderController = new Mock<IOrderController>();
            Mock<IProductController> _MockProductController = new Mock<IProductController>();

            List<Supplier> _Suppliers = new List<Supplier> {
                new Supplier
                {
                    Email = "test@email.com"
                }
            };

            _MockRepository.Setup(mock => mock.GetAll()).Returns(_Suppliers);

            ISupplierController _SupplierController = new SupplierController(_MockRepository.Object, _MockProductController.Object, _MockOrderController.Object);

            bool _Result = _SupplierController.IsEmailInUse("test@email.com");

            Assert.AreEqual(true, _Result);

            _MockRepository.Verify(mock => mock.GetAll(), Times.Once);
        }

        [TestMethod]
        public void SupplierController_IsNameInUse_ReturnsFalseIfNotInUse ()
        {
            Mock<ISupplierRepository> _MockRepository = new Mock<ISupplierRepository>();
            Mock<IOrderController> _MockOrderController = new Mock<IOrderController>();
            Mock<IProductController> _MockProductController = new Mock<IProductController>();

            List<Supplier> _Suppliers = new List<Supplier> {
                new Supplier
                {
                    Name= "Test Supplier 53",
                }
            };

            _MockRepository.Setup(mock => mock.GetAll()).Returns(_Suppliers);

            ISupplierController _SupplierController = new SupplierController(_MockRepository.Object, _MockProductController.Object, _MockOrderController.Object);

            bool _Result = _SupplierController.IsNameInUse("Test Supplier 22");

            Assert.AreEqual(false, _Result);

            _MockRepository.Verify(mock => mock.GetAll(), Times.Once);
        }

        [TestMethod]
        public void SupplierController_IsNameInUse_ReturnsTrueIfInUse ()
        {
            Mock<ISupplierRepository> _MockRepository = new Mock<ISupplierRepository>();
            Mock<IOrderController> _MockOrderController = new Mock<IOrderController>();
            Mock<IProductController> _MockProductController = new Mock<IProductController>();

            List<Supplier> _Suppliers = new List<Supplier> {
                new Supplier
                {
                    Name= "Test Supplier",
                }
            };

            _MockRepository.Setup(mock => mock.GetAll()).Returns(_Suppliers);

            ISupplierController _SupplierController = new SupplierController(_MockRepository.Object, _MockProductController.Object, _MockOrderController.Object);

            bool _Result = _SupplierController.IsNameInUse("Test Supplier");

            Assert.AreEqual(true, _Result);

            _MockRepository.Verify(mock => mock.GetAll(), Times.Once);
        }

        [TestMethod]
        public void SupplierController_IsSupplierInUse_ReturnsFalseIfNotInUse ()
        {
            Mock<ISupplierRepository> _MockRepository = new Mock<ISupplierRepository>();
            Mock<IOrderController> _MockOrderController = new Mock<IOrderController>();
            Mock<IProductController> _MockProductController = new Mock<IProductController>();

            List<Product> _Products = new List<Product>();
            List<Order> _Orders = new List<Order>();

            _MockProductController
                .Setup(mock => mock.GetProductsBySupplier(It.IsAny<Guid>()))
                .Returns(_Products);

            _MockOrderController
                .Setup(mock => mock.GetOrdersBySupplier(It.IsAny<Guid>()))
                .Returns(_Orders);

            ISupplierController _SupplierController = new SupplierController(_MockRepository.Object, _MockProductController.Object, _MockOrderController.Object);

            bool _Result = _SupplierController.IsSupplierInUse(Guid.NewGuid());

            Assert.AreEqual(false, _Result);

            _MockProductController.Verify(mock => mock.GetProductsBySupplier(It.IsAny<Guid>()), Times.Once);
            _MockOrderController.Verify(mock => mock.GetOrdersBySupplier(It.IsAny<Guid>()), Times.Once);
        }

        [TestMethod]
        public void SupplierController_IsSupplierInUse_ReturnsTrueIfInUse ()
        {
            Mock<ISupplierRepository> _MockRepository = new Mock<ISupplierRepository>();
            Mock<IOrderController> _MockOrderController = new Mock<IOrderController>();
            Mock<IProductController> _MockProductController = new Mock<IProductController>();

            List<Product> _Products = new List<Product>
            {
                new Product()
            };

            List<Order> _Orders = new List<Order>
            {
                new Order()
            };

            _MockProductController
                .Setup(mock => mock.GetProductsBySupplier(It.IsAny<Guid>()))
                .Returns(_Products);

            _MockOrderController
                .Setup(mock => mock.GetOrdersBySupplier(It.IsAny<Guid>()))
                .Returns(_Orders);

            ISupplierController _SupplierController = new SupplierController(_MockRepository.Object, _MockProductController.Object, _MockOrderController.Object);

            bool _Result = _SupplierController.IsSupplierInUse(Guid.NewGuid());

            Assert.AreEqual(true, _Result);

            _MockProductController.Verify(mock => mock.GetProductsBySupplier(It.IsAny<Guid>()), Times.Once);
            _MockOrderController.Verify(mock => mock.GetOrdersBySupplier(It.IsAny<Guid>()), Times.Once);
        }
    }
}
