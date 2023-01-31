using FutureFridges.Business.Enums;

namespace FutureFridges.Business.StockManagement
{
    public class Product
    {
        public ProductCategory Category { get; set; }
        public int DaysShelfLife { get; set; }
        public int Id { get; set; }
        public string? ImageName { get; set; }
        public int MinimumStockLevel { get; set; }
        public string? Name { get; set; }
        public Guid Supplier_UID { get; set; }
        public Guid UID { get; set; }
    }
}
