using FutureFridges.Business.StockManagement;

namespace FutureFridges.Data.StockManagement
{
    public class StockItemRepository : IStockItemRepository
    {
        //DATABASE CONNECTION VARIABLES GO UP HERE

        public StockItem GetStockItem(int stockItemID)
        {
            //GET STOCK ITEM FROM DATABASE, CONVERT TO LOCAL STOCK ITEM CLASS AND RETURN

            return new StockItem(); //TEMPORARY RETURN
        }
    }
}
