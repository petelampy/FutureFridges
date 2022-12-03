namespace FutureFridges.Business.StockManagement
{
    public interface IProductController
    {
        IEnumerable<Product> GetAll ();
        Product GetProduct (Guid product_UID);
    }
}