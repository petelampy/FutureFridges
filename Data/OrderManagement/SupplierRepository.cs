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

        public void Create (Supplier supplier)
        {
            __DbContext.Suppliers.Add(supplier);
            __DbContext.SaveChanges();
        }

        public void Delete (Guid uid)
        {
            Supplier _Supplier = Get(uid);

            __DbContext.Remove(_Supplier);
            __DbContext.SaveChanges();
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

        public void Update (Supplier updatedSupplier)
        {
            Supplier _Supplier = Get(updatedSupplier.UID);

            _Supplier.Name = updatedSupplier.Name;
            _Supplier.Email = updatedSupplier.Email;

            __DbContext.SaveChanges();
        }
    }
}
