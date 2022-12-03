namespace FutureFridges.Business.StockManagement
{
    public interface IProductController
    {
        List<Product> GetAll ();
        Product GetProduct (Guid product_UID);
    }
}