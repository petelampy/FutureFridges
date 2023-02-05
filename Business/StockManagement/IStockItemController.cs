namespace FutureFridges.Business.StockManagement
{
    public interface IStockItemController
    {
        void CreateStockItem (List<StockItem> stockItems);
        void CreateStockItem (StockItem stockItem);
        void DeleteStockItem (Guid stockItem_UID);
        List<StockItem> GetAll ();
        StockItem GetStockItem (Guid stockItem_UID);
        List<StockItem> GetStockItemsByProduct (Guid product_UID);
    }
}