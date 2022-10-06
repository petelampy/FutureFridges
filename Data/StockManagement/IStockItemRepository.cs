using FutureFridges.Business.StockManagement;

namespace FutureFridges.Data.StockManagement
{
    public interface IStockItemRepository
    {
        StockItem GetStockItem(int stockItemID);
    }
}