using FutureFridges.Business.StockManagement;

namespace FutureFridges.Data.StockManagement
{
    public interface IStockItemRepository
    {
        void CreateStockItem (List<StockItem> stockItems);
        void CreateStockItem (StockItem stockItem);
        void DeleteStockItem (Guid stockitem_UID);
        List<StockItem> GetAll ();
        StockItem GetStockItem (Guid stockItem_UID);
        List<StockItem> GetStockItemsByProduct (Guid product_UID);
    }
}