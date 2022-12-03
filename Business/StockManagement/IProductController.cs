namespace FutureFridges.Business.StockManagement
{
    public interface IProductController
    {
        void CreateProduct (Product newProduct);
        List<Product> GetAll ();
        Product GetProduct (Guid product_UID);
    }
}