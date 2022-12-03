namespace FutureFridges.Business.StockManagement
{
    public interface IStockItemController
    {
        StockItem GetStockItem (Guid stockItem_UID);
    }
}