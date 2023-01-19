using FutureFridges.Business.StockManagement;
using FutureFridges.Data.OrderManagement;

namespace FutureFridges.Business.OrderManagement
{
    public class SupplierController : ISupplierController
    {
        private readonly IProductController __ProductController;
        private readonly ISupplierRepository __SupplierRepository;

        public SupplierController ()
            : this(new SupplierRepository(), new ProductController())
        { }

        internal SupplierController (ISupplierRepository supplierRepository, IProductController productController)
        {
            __SupplierRepository = supplierRepository;
            __ProductController = productController;
        }

        public Supplier Get (Guid uid)
        {
            return __SupplierRepository.Get(uid);
        }

        public List<Supplier> GetAll ()
        {
            return __SupplierRepository.GetAll();
        }

        public Supplier GetByProduct (Guid product_uid)
        {
            Guid _Supplier_UID = __ProductController.GetProduct(product_uid).Supplier_UID;

            return Get(_Supplier_UID);
        }
    }
}
