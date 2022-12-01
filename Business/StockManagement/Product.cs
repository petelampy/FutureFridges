using FutureFridges.Business.Enums;

namespace FutureFridges.Business.StockManagement
{
    public class Product
    {
        public string? Name { get; set; }
        public StockCategory Category { get; set; }
    }
}
