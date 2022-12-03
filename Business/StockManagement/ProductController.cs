using FutureFridges.Data.StockManagement;
using FutureFridges.Data;
using System.Runtime.CompilerServices;

namespace FutureFridges.Business.StockManagement
{
    public class ProductController : IProductController
    {
        private readonly IProductRepository __ProductRepository;

        public ProductController ()
            : this(new ProductRepository())
        { }

        internal ProductController (IProductRepository productRepository)
        {
            __ProductRepository = productRepository;
        }

        public Product GetProduct (Guid product_UID)
        {
            return __ProductRepository.GetProduct(product_UID);
        }

        public List<Product> GetAll ()
        {
            return __ProductRepository.GetAll();
        }

        public void UpdateProduct(Product updatedProduct)
        {
            __ProductRepository.UpdateProduct(updatedProduct);
        }
    }
}
