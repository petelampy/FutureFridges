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

        public void CreateStockItem (StockItem stockItem)
        {
            __DbContext.StockItems.Add(stockItem);
            __DbContext.SaveChanges();
        }

        public void CreateStockItem (List<StockItem> stockItems)
        {
            __DbContext.StockItems.AddRange(stockItems);
            __DbContext.SaveChanges();
        }

        public void DeleteStockItem (Guid stockitem_UID)
        {
            StockItem _CurrentStockItem = GetStockItem(stockitem_UID);

            __DbContext.Remove(_CurrentStockItem);
            __DbContext.SaveChanges();
        }

        public List<StockItem> GetAll ()
        {
            return __DbContext.StockItems.ToList();
        }

        public StockItem GetStockItem (Guid stockitem_UID)
        {
            return __DbContext.StockItems
                .ToList()
                .Where(stockItem => stockItem.Item_UID == stockitem_UID)
                .SingleOrDefault(new StockItem());
        }

        public List<StockItem> GetStockItemsByProduct (Guid product_UID)
        {
            return __DbContext.StockItems
                .Where(stockItem => stockItem.Product_UID == product_UID)
                .ToList();
        }
    }
}
