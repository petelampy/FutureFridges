using FutureFridges.Data.StockManagement;

namespace FutureFridges.Business.StockManagement
{
    public class StockItemController : IStockItemController
    {
        private readonly IStockItemRepository __StockItemRepository;

        public StockItemController ()
            : this(new StockItemRepository())
        { }

        internal StockItemController (IStockItemRepository stockItemRepository)
        {
            __StockItemRepository = stockItemRepository;
        }

        public List<StockItem> GetAll ()
        {
            return __StockItemRepository.GetAll();
        }

        public StockItem GetStockItem (Guid stockItem_UID)
        {
            return __StockItemRepository.GetStockItem(stockItem_UID);
        }

        public StockItem GetStockItemByProduct (Guid product_UID)
        {
            return __StockItemRepository.GetStockItemByProduct(product_UID);
        }

        public void DeleteStockItem (Guid stockItem_UID)
        {
            __StockItemRepository.DeleteStockItem(stockItem_UID);
        }
    }
}
