using FutureFridges.Business.Enums;

namespace FutureFridges.Business.StockManagement
{
    public class Product
    {
        public StockCategory Category { get; set; }
        public string? Name { get; set; }
        public Guid Product_UID { get; set; }
    }
}
