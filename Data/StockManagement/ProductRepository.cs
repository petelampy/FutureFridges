using FutureFridges.Business.StockManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace FutureFridges.Data.StockManagement
{
    public class ProductRepository : IProductRepository
    {
        private readonly FridgeDBContext __DbContext;
        private readonly DbContextInitialiser __DbContextInitialiser;

        public ProductRepository() {
            __DbContextInitialiser = new DbContextInitialiser();
            __DbContext = __DbContextInitialiser.CreateNewDbContext();
        }

        public Product GetProduct (Guid product_UID)
        {
            return __DbContext.Products
                .AsEnumerable()
                .Where(product => product.Product_UID == product_UID)
                .SingleOrDefault(new Product());
        }

        public IEnumerable<Product> GetAll ()
        {
            return __DbContext.Products.ToList();
        }
    }
}
