namespace FutureFridges.Business.StockManagement
{
    public interface IStockItemController
    {
        void CreateStockItem (List<StockItem> stockItems);
        void CreateStockItem (StockItem stockItem);
        void DeleteStockItem (Guid stockItem_UID);
        StockItem GetStockItem (Guid stockItem_UID);
    }
}