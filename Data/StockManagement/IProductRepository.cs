using FutureFridges.Business.StockManagement;

namespace FutureFridges.Data.StockManagement
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll ();
        Product GetProduct (Guid product_UID);
    }
}