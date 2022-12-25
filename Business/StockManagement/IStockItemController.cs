namespace FutureFridges.Business.StockManagement
{
    public interface IStockItemController
    {
        void DeleteStockItem (Guid stockItem_UID);
        StockItem GetStockItem (Guid stockItem_UID);
    }
}