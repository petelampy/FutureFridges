using FutureFridges.Business.OrderManagement;

namespace FutureFridges.Data.OrderManagement
{
    public interface ISupplierRepository
    {
        void Create (Supplier supplier);
        Supplier Get (Guid uid);
        List<Supplier> GetAll ();
        void Update (Supplier updatedSupplier);
    }
}