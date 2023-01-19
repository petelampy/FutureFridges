using FutureFridges.Business.StockManagement;

namespace FutureFridges.Data.StockManagement
{
    public interface IProductRepository
    {
        void CreateProduct (Product newProduct);
        void DeleteProduct (Guid uid);
        List<Product> GetAll ();
        Product GetProduct (Guid product_UID);
        void UpdateProduct (Product updatedProduct);
    }
}