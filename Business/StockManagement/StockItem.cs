namespace FutureFridges.Business.StockManagement
{
    public class StockItem
    {
        public DateTime ExpiryDate { get; set; }
        public int Id { get; set; }
        public Guid Item_UID { get; set; }
        public Guid Product_UID { get; set; }
    }
}
