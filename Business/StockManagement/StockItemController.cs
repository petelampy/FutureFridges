using FutureFridges.Data.StockManagement;

namespace FutureFridges.Business.StockManagement
{
    public class StockItemController : IStockItemController
    {
        private readonly IStockItemRepository __StockItemRepository;

        public StockItemController()
            : this(new StockItemRepository())
        { }

        internal StockItemController(IStockItemRepository stockItemRepository)
        {
            __StockItemRepository = stockItemRepository;
        }

        public StockItem GetStockItem(Guid stockItem_UID)
        {
            return __StockItemRepository.GetStockItem(stockItem_UID);
        }

        public IEnumerable<StockItem> GetAll()
        {
            return __StockItemRepository.GetAll();
        }
    }
}
