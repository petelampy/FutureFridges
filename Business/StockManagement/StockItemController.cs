using FutureFridges.Data.StockManagement;

namespace FutureFridges.Business.StockManagement
{
    public class StockItemController : IStockItemController
    {
        private readonly IStockItemRepository __StockItemRepository;

        public StockItemController(IStockItemRepository stockItemRepository)
        {
            __StockItemRepository = stockItemRepository;
        }

        public StockItem GetStockItem(int stockItemID)
        {
            StockItem _StockItem = __StockItemRepository.GetStockItem(stockItemID);

            return _StockItem;
        }
    }
}
