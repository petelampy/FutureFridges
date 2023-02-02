using FutureFridges.Business.OrderManagement;
using FutureFridges.Data.StockManagement;

namespace FutureFridges.Business.StockManagement
{
    public class ProductController : IProductController
    {
        private readonly IOrderController __OrderController;
        private readonly IProductRepository __ProductRepository;
        private readonly IStockItemController __StockItemController;

        public ProductController ()
            : this(new ProductRepository(), new StockItemController(), new OrderController())
        { }

        internal ProductController (IProductRepository productRepository, IStockItemController stockItemController, IOrderController orderController)
        {
            __ProductRepository = productRepository;
            __StockItemController = stockItemController;
            __OrderController = orderController;
        }

        public void CreateProduct (Product newProduct)
        {
            newProduct.UID = Guid.NewGuid();

            __ProductRepository.CreateProduct(newProduct);
        }

        public void DeleteProduct (Guid uid)
        {
            __ProductRepository.DeleteProduct(uid);
        }

        public List<Product> GetAll ()
        {
            return __ProductRepository.GetAll();
        }

        public Product GetProduct (Guid uid)
        {
            return __ProductRepository.GetProduct(uid);
        }

        public List<Product> GetProducts (List<Guid> uids)
        {
            return __ProductRepository.GetProducts(uids);
        }

        public List<Product> GetProductsBySupplier (Guid supplier_uid)
        {
            return __ProductRepository.GetProductsBySupplier(supplier_uid);
        }

        public bool IsProductInUse (Guid uid)
        {
            List<StockItem> _Stock = __StockItemController.GetStockItemsByProduct(uid);
            List<OrderItem> _OrderItems = __OrderController.GetOrderItemsByProduct(uid);

            return _Stock.Count > 0 || _OrderItems.Count > 0;
        }

        public void UpdateProduct (Product updatedProduct)
        {
            __ProductRepository.UpdateProduct(updatedProduct);
        }
    }
}
