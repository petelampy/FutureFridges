using FutureFridges.Data.StockManagement;

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

        public List<Product> GetAll ()
        {
            return __ProductRepository.GetAll();
        }

        public Product GetProduct (Guid product_UID)
        {
            return __ProductRepository.GetProduct(product_UID);
        }

        public void UpdateProduct (Product updatedProduct)
        {
            __ProductRepository.UpdateProduct(updatedProduct);
        }
    }
}
