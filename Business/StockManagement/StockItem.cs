namespace FutureFridges.Business.StockManagement
{
    public class StockItem
    {
        public decimal AmountOnOrder { get; set; }
        public decimal AmountRemaining { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
