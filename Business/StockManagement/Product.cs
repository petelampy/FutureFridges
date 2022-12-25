using FutureFridges.Business.Enums;

namespace FutureFridges.Business.StockManagement
{
    public class Product
    {
        public string? ImageName { get; set; }
        public ProductCategory Category { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public Guid Product_UID { get; set; }
        //MAYBE RENAME THE UIDS TO JUST BE "UID"??
    }
}
