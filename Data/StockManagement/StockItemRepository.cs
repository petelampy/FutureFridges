using FutureFridges.Business.StockManagement;

namespace FutureFridges.Data.StockManagement
{
    public class StockItemRepository : IStockItemRepository
    {
        private readonly FridgeDBContext __DbContext;
        private readonly IDbContextInitialiser __DbContextInitialiser;

        public StockItemRepository () :
            this(new DbContextInitialiser())
        { }

        internal StockItemRepository (IDbContextInitialiser dbContextInitialiser)
        {
            __DbContextInitialiser = dbContextInitialiser;
            __DbContext = __DbContextInitialiser.CreateNewDbContext();
        }

        public IEnumerable<StockItem> GetAll ()
        {
            return __DbContext.StockItems.ToList();
        }

        public StockItem GetStockItem (Guid stockitem_UID)
        {
            return __DbContext.StockItems
                .Where(stockItem => stockItem.Item_UID == stockitem_UID)
                .SingleOrDefault(new StockItem());
        }
    }
}
