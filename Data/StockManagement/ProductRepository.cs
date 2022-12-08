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
            //SET THE RETURN AS A BOOL, THROW AN ERROR IF IT FAILS TO CREATE?
            newProduct.Product_UID = Guid.NewGuid();

            __DbContext.Products.Add(newProduct);
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
                .Where(product => product.Product_UID == product_UID)
                .SingleOrDefault(new Product());
        }

        public void UpdateProduct (Product updatedProduct)
        {
            //SET THE RETURN AS A BOOL, THROW AN ERROR IF IT FAILS TO UPDATE?

            Product _CurrentProduct = GetProduct(updatedProduct.Product_UID);

            _CurrentProduct.Name = updatedProduct.Name;
            _CurrentProduct.Category = updatedProduct.Category;

            __DbContext.SaveChanges();
        }
    }
}
