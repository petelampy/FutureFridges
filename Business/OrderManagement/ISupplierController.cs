namespace FutureFridges.Business.OrderManagement
{
    public interface ISupplierController
    {
        Supplier Get (Guid uid);
        List<Supplier> GetAll ();
        Supplier GetByProduct (Guid product_uid);
    }
}