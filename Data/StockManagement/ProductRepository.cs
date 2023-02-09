using FutureFridges.Business.StockManagement;

namespace FutureFridges.Data.StockManagement
{
    public class ProductRepository : IProductRepository
    {
        private readonly FridgeDBContext __DbContext;
        private readonly IDbContextInitialiser __DbContextInitialiser;

        public ProductRepository () :
            this(new DbContextInitialiser())
        { }

        internal ProductRepository (IDbContextInitialiser dbContextInitialiser)
        {
            __DbContextInitialiser = dbContextInitialiser;
            __DbContext = __DbContextInitialiser.CreateNewDbContext();
        }

        public void CreateProduct (Product newProduct)
        {
            __DbContext.Products.Add(newProduct);
            __DbContext.SaveChanges();
        }

        public void DeleteProduct (Guid uid)
        {
            Product _Product = GetProduct(uid);

            __DbContext.Remove(_Product);
            __DbContext.SaveChanges();
        }

        public List<Product> GetAll ()
        {
            return __DbContext.Products.ToList();
        }

        public Product GetProduct (Guid product_UID)
        {
            return __DbContext.Products
                .AsEnumerable()
                .Where(product => product.UID == product_UID)
                .SingleOrDefault(new Product());
        }

        public List<Product> GetProducts (List<Guid> uids)
        {
            return __DbContext.Products
                .AsEnumerable()
                .Where(product => uids.Contains(product.UID))
                .ToList();
        }

        public List<Product> GetProductsBySupplier (Guid supplier_UID)
        {
            return __DbContext
                .Products
                .Where(product => product.Supplier_UID == supplier_UID)
                .ToList();
        }

        public void UpdateProduct (Product updatedProduct)
        {
            Product _CurrentProduct = GetProduct(updatedProduct.UID);

            _CurrentProduct.Name = updatedProduct.Name;
            _CurrentProduct.Category = updatedProduct.Category;
            _CurrentProduct.ImageName = updatedProduct.ImageName;
            _CurrentProduct.DaysShelfLife = updatedProduct.DaysShelfLife;
            _CurrentProduct.MinimumStockLevel = updatedProduct.MinimumStockLevel;

            __DbContext.SaveChanges();
        }
    }
}
