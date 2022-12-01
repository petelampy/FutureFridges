namespace FutureFridges.Business.StockManagement
{
    public class StockItem
    {
        public int Id { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Guid Item_UID { get; set; }
        public Product? Product { get; set; }
    }
}
