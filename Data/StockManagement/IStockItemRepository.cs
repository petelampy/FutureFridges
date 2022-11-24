using FutureFridges.Business.StockManagement;

namespace FutureFridges.Data.StockManagement
{
    public interface IStockItemRepository
    {
        IEnumerable<StockItem> GetAll();
        StockItem GetStockItem(Guid stockItem_UID);
    }
}