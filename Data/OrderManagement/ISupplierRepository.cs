using FutureFridges.Business.OrderManagement;

namespace FutureFridges.Data.OrderManagement
{
    public interface ISupplierRepository
    {
        Supplier Get (Guid uid);
        List<Supplier> GetAll ();
    }
}