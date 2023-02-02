using FutureFridges.Business.OrderManagement;

namespace FutureFridges.Data.OrderManagement
{
    public interface ISupplierRepository
    {
        void Create (Supplier supplier);
        void Delete (Guid uid);
        Supplier Get (Guid uid);
        List<Supplier> GetAll ();
        void Update (Supplier updatedSupplier);
    }
}