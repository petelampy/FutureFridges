namespace FutureFridges.Business.OrderManagement
{
    public interface ISupplierController
    {
        void Create (Supplier supplier);
        Supplier Get (Guid uid);
        List<Supplier> GetAll ();
        Supplier GetByProduct (Guid product_uid);
        void Update (Supplier updatedSupplier);
    }
}