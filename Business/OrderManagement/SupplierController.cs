using FutureFridges.Business.StockManagement;
using FutureFridges.Business.UserManagement;
using FutureFridges.Data.OrderManagement;

namespace FutureFridges.Business.OrderManagement
{
    public class SupplierController : ISupplierController
    {
        private readonly IOrderController __OrderController;
        private readonly IProductController __ProductController;
        private readonly ISupplierRepository __SupplierRepository;

        public SupplierController ()
            : this(new SupplierRepository(), new ProductController(), new OrderController())
        { }

        internal SupplierController (ISupplierRepository supplierRepository, IProductController productController, IOrderController orderController)
        {
            __SupplierRepository = supplierRepository;
            __ProductController = productController;
            __OrderController = orderController;
        }

        public void Create (Supplier supplier)
        {
            supplier.UID = Guid.NewGuid();

            __SupplierRepository.Create(supplier);
        }

        public void Delete (Guid uid)
        {
            __SupplierRepository.Delete(uid);
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

        public bool IsEmailInUse (string email)
        {
            List<Supplier> _Suppliers = __SupplierRepository.GetAll();

            return _Suppliers.Count(supplier => supplier.Email.ToUpperInvariant() == email.ToUpperInvariant()) > 0;
        }

        public bool IsNameInUse (string name)
        {
            List<Supplier> _Suppliers = __SupplierRepository.GetAll();

            return _Suppliers.Count(supplier => supplier.Name.ToUpperInvariant() == name.ToUpperInvariant()) > 0;
        }
        
        public bool IsSupplierInUse (Guid uid)
        {
            List<Product> _Products = __ProductController.GetProductsBySupplier(uid);
            List<Order> _Orders = __OrderController.GetOrdersBySupplier(uid);

            return _Products.Count > 0 || _Orders.Count > 0;
        }

        public void Update (Supplier updatedSupplier)
        {
            __SupplierRepository.Update(updatedSupplier);
        }
    }
}
