namespace FutureFridges.Business.StockManagement
{
    public interface IProductController
    {
        void CreateProduct (Product newProduct);
        void DeleteProduct (Guid uid);
        List<Product> GetAll ();
        Product GetProduct (Guid product_UID);
        bool IsProductInUse (Guid uid);
    }
}