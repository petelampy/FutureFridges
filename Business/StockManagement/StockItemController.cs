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

        public void CreateStockItem (StockItem stockItem)
        {
            stockItem.Item_UID = Guid.NewGuid();

            __StockItemRepository.CreateStockItem(stockItem);
        }

        public void CreateStockItem (List<StockItem> stockItems)
        {
            foreach (StockItem _StockItem in stockItems)
            {
                _StockItem.Item_UID = Guid.NewGuid();
            }

            __StockItemRepository.CreateStockItem(stockItems);
        }

        public void DeleteStockItem (Guid stockItem_UID)
        {
            __StockItemRepository.DeleteStockItem(stockItem_UID);
        }

        public List<StockItem> GetAll ()
        {
            return __StockItemRepository.GetAll();
        }

        public StockItem GetStockItem (Guid stockItem_UID)
        {
            return __StockItemRepository.GetStockItem(stockItem_UID);
        }

        public List<StockItem> GetStockItemsByProduct (Guid product_UID)
        {
            return __StockItemRepository.GetStockItemsByProduct(product_UID);
        }
    }
}
