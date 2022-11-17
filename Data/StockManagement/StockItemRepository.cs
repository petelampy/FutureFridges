using FutureFridges.Business.StockManagement;

namespace FutureFridges.Data.StockManagement
{
    public class StockItemRepository : IStockItemRepository
    {
        //DATABASE CONNECTION VARIABLES GO UP HERE

        public StockItem GetStockItem(Guid stockitem_UID)
        {
            //GET STOCK ITEM FROM DATABASE, CONVERT TO LOCAL STOCK ITEM CLASS AND RETURN

            return new StockItem(); //TEMPORARY RETURN
        }

        public IEnumerable<StockItem> GetAll()
        {
            //FETCH EVERYTHING FROM THE TABLE AND RETURN

            return new List<StockItem>();
        }
    }
}
