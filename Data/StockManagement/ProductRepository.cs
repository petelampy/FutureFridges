using FutureFridges.Business.StockManagement;

namespace FutureFridges.Data.StockManagement
{
    public class ProductRepository : IProductRepository
    {
        //DATABASE CONNECTION VARIABLES GO UP HERE

        public Product GetProduct (Guid product_UID)
        {
            //GET STOCK ITEM FROM DATABASE, CONVERT TO LOCAL STOCK ITEM CLASS AND RETURN

            return new Product(); //TEMPORARY RETURN
        }

        public IEnumerable<Product> GetAll ()
        {
            //FETCH EVERYTHING FROM THE TABLE AND RETURN

            return new List<Product>();
        }
    }
}
