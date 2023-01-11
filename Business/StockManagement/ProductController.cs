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

        public void CreateProduct (Product newProduct)
        {
            newProduct.UID = Guid.NewGuid();

            __ProductRepository.CreateProduct(newProduct);
        }

        public List<Product> GetAll ()
        {
            return __ProductRepository.GetAll();
        }

        public Product GetProduct (Guid uid)
        {
            return __ProductRepository.GetProduct(uid);
        }

        public void UpdateProduct (Product updatedProduct)
        {
            __ProductRepository.UpdateProduct(updatedProduct);
        }
    }
}
