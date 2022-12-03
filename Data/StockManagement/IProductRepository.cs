using FutureFridges.Business.StockManagement;

namespace FutureFridges.Data.StockManagement
{
    public interface IProductRepository
    {
        List<Product> GetAll ();
        Product GetProduct (Guid product_UID);
        void UpdateProduct (Product updatedProduct);
    }
}