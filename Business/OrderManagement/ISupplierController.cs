namespace FutureFridges.Business.OrderManagement
{
    public interface ISupplierController
    {
        void Create (Supplier supplier);
        void Delete (Guid uid);
        Supplier Get (Guid uid);
        List<Supplier> GetAll ();
        Supplier GetByProduct (Guid product_uid);
        bool IsEmailInUse (string email);
        bool IsNameInUse (string name);
        bool IsSupplierInUse (Guid uid);
        void Update (Supplier updatedSupplier);
    }
}