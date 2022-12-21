using FutureFridges.Business.StockManagement;

namespace FutureFridges.Data.StockManagement
{
    public interface IStockItemRepository
    {
        List<StockItem> GetAll ();
        StockItem GetStockItem (Guid stockItem_UID);
        StockItem GetStockItemByProduct (Guid product_UID);
    }
}