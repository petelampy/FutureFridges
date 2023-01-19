using FutureFridges.Business.OrderManagement;

namespace FutureFridges.Data.OrderManagement
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly FridgeDBContext __DbContext;
        private readonly IDbContextInitialiser __DbContextInitialiser;

        public SupplierRepository () :
            this(new DbContextInitialiser())
        { }

        internal SupplierRepository (IDbContextInitialiser dbContextInitialiser)
        {
            __DbContextInitialiser = dbContextInitialiser;
            __DbContext = __DbContextInitialiser.CreateNewDbContext();
        }

        public Supplier Get (Guid uid)
        {
            return __DbContext.Suppliers
                .ToList()
                .Where(supplier => supplier.UID == uid)
                .SingleOrDefault(new Supplier());
        }

        public List<Supplier> GetAll ()
        {
            return __DbContext.Suppliers
                .ToList();
        }
    }
}
