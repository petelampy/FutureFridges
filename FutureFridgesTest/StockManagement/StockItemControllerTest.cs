using FutureFridges.Business.StockManagement;
using FutureFridges.Data.StockManagement;

namespace FutureFridgesTest.StockManagement
{
    [TestClass]
    public class StockItemControllerTest
    {
        [TestMethod]
        public void StockItemController_Create_GeneratesValidStockItem ()
        {
            Mock<IStockItemRepository> _MockRepository = new Mock<IStockItemRepository>();

            StockItem _Result = new StockItem();
            
            _MockRepository.Setup(mock => mock.CreateStockItem(It.IsAny<StockItem>())).Callback((StockItem stockitem) =>
            {
                _Result = stockitem;
            }).Verifiable();
            
            IStockItemController _StockItemController = new StockItemController(_MockRepository.Object);

            StockItem _StockItem = new StockItem
            {
                ExpiryDate = DateTime.Now,
                Id = 25,
                Product_UID = Guid.NewGuid()
            };
            
            _StockItemController.CreateStockItem(_StockItem);

            _MockRepository.Verify(repo => repo.CreateStockItem(It.IsAny<StockItem>()), Times.Once());

            Assert.AreNotEqual(Guid.Empty, _Result.Item_UID);

            Assert.AreEqual(_StockItem.Id, _Result.Id);
            Assert.AreEqual(_StockItem.ExpiryDate, _Result.ExpiryDate);
            Assert.AreEqual(_StockItem.Product_UID, _Result.Product_UID);
        }
    }
}