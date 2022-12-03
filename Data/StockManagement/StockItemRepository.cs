using FutureFridges.Business.StockManagement;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;

namespace FutureFridges.Data.StockManagement
{
    public class StockItemRepository : IStockItemRepository
    {
        private readonly FridgeDBContext __DbContext;
        private readonly DbContextInitialiser __DbContextInitialiser;

        public StockItemRepository()
        {
            __DbContextInitialiser = new DbContextInitialiser();
            __DbContext = __DbContextInitialiser.CreateNewDbContext();
        }

        public StockItem GetStockItem(Guid stockitem_UID)
        {
            return __DbContext.StockItems
                .Where(stockItem => stockItem.Item_UID == stockitem_UID)
                .SingleOrDefault(new StockItem());
        }

        public IEnumerable<StockItem> GetAll()
        {
            return __DbContext.StockItems.ToList();
        }
    }
}
