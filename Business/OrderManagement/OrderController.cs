using FutureFridges.Business.Email;
using FutureFridges.Data.OrderManagement;
using FutureFridges.Data.StockManagement;
using System.Text;

namespace FutureFridges.Business.OrderManagement
{
    public class OrderController : IOrderController
    {
        private const string SUPPLIER_ORDER_EMAIL_SUBJECT = "Future Fridges - You've received an order!";

        private readonly IEmailManager __EmailManager;
        private readonly IOrderRepository __OrderRepository;
        private readonly IProductRepository __ProductRepository;
        private readonly ISupplierRepository __SupplierRepository;

        public OrderController ()
            : this(new OrderRepository(), new ProductRepository(), new SupplierRepository(), new EmailManager())
        { }

        internal OrderController (IOrderRepository orderRepository, IProductRepository productRepository, ISupplierRepository supplierRepository, IEmailManager emailManager)
        {
            __OrderRepository = orderRepository;
            __ProductRepository = productRepository;
            __SupplierRepository = supplierRepository;
            __EmailManager = emailManager;
        }

        public void ClearUnfinishedOrders()
        {
            List<Order> _UnfinishedOrders = __OrderRepository
                .GetAll()
                .Where(order => order.Supplier_UID == null || order.Supplier_UID == Guid.Empty)
                .ToList();
            
            foreach (Order _Order in _UnfinishedOrders)
            {
                DeleteOrder(_Order.UID);
            }
        }

        public void CompleteOrder (Guid uid)
        {
            Order _Order = GetOrder(uid);

            List<Guid> _Supplier_UIDs = _Order.OrderItems
                .Select(orderItem => orderItem.Supplier_UID)
                .Distinct()
                .ToList();

            foreach (Guid _Supplier_UID in _Supplier_UIDs)
            {
                Order _SupplierOrder = new Order { Supplier_UID = _Supplier_UID };
                CreateOrder(_SupplierOrder);

                List<OrderItem> _SupplierOrderItems = _Order.OrderItems
                    .Where(orderItem => orderItem.Supplier_UID == _Supplier_UID)
                    .ToList();

                foreach (OrderItem _Item in _SupplierOrderItems)
                {
                    CreateOrderItem(new OrderItem
                    {
                        Order_UID = _SupplierOrder.UID,
                        ProductName = _Item.ProductName,
                        Product_UID = _Item.Product_UID,
                        Quantity = _Item.Quantity,
                        Supplier_UID = _Item.Supplier_UID
                    });
                }

                SendSupplierOrderEmail(_SupplierOrderItems, _Supplier_UID, _SupplierOrder.PinCode);
                __OrderRepository.UpdateItemCount(_SupplierOrder.UID);
            }

            DeleteOrder(_Order.UID);
        }

        public void CreateOrder (Order order)
        {
            order.UID = Guid.NewGuid();
            order.PinCode = CreatePinCode();
            order.NumberOfItems = order.OrderItems == null ? 0 : order.OrderItems.Select(orderItem => orderItem.Quantity).Sum();

            __OrderRepository.CreateOrder(order);
        }

        public void CreateOrderItem (OrderItem orderItem)
        {
            orderItem.UID = Guid.NewGuid();

            __OrderRepository.CreateOrderItem(orderItem);
        }

        internal int CreatePinCode ()
        {
            List<int> _ValidPinCodes = Enumerable.Range(1000, 8999).ToList();

            List<int> _UsedPinCodes = __OrderRepository
                .GetAll()
                .Select(order => order.PinCode)
                .ToList();

            List<int> _AvailablePinCodes = _ValidPinCodes.Except(_UsedPinCodes).ToList();

            Random _Random = new Random();
            int _SelectedPinIndex = _Random.Next(_AvailablePinCodes.Count());

            return _AvailablePinCodes[_SelectedPinIndex];
        }

        public void DeleteOrder (Guid uid)
        {
            __OrderRepository.DeleteOrder(uid);
        }

        public void DeleteOrderItem (Guid uid)
        {
            __OrderRepository.DeleteOrderItem(uid);
        }

        public List<Order> GetAll ()
        {
            List<Order> _Orders = __OrderRepository.GetAll();

            foreach (Order _Order in _Orders)
            {
                _Order.OrderItems = GetItemProductNames(_Order.OrderItems);
            }

            return _Orders;
        }

        private List<OrderItem> GetItemProductNames (List<OrderItem> orderItems)
        {
            foreach (OrderItem _OrderItem in orderItems)
            {
                _OrderItem.ProductName = __ProductRepository.GetProduct(_OrderItem.Product_UID).Name;
            }

            return orderItems;
        }

        public Order GetOrder (Guid uid)
        {
            Order _Order = __OrderRepository.GetOrder(uid);

            _Order.OrderItems = GetItemProductNames(_Order.OrderItems);

            return _Order;
        }

        public Order GetOrderByPinCode (int pinCode)
        {
            Order _Order = __OrderRepository.GetOrderByPinCode(pinCode);

            _Order.OrderItems = GetItemProductNames(_Order.OrderItems);

            return _Order;
        }

        public List<OrderItem> GetOrderItems (Guid order_uid)
        {
            List<OrderItem> _OrderItems = __OrderRepository.GetOrderItems(order_uid);

            _OrderItems = GetItemProductNames(_OrderItems);

            return _OrderItems;
        }

        public List<OrderItem> GetOrderItemsByProduct (Guid product_UID)
        {
            return __OrderRepository.GetOrderItemsByProduct(product_UID);
        }

        public List<Order> GetOrdersBySupplier (Guid supplier_UID)
        {
            return __OrderRepository.GetOrdersBySupplier(supplier_UID);
        }

        public bool IsValidOrderPinCode (int pinCode)
        {
            return __OrderRepository.IsValidOrderPinCode(pinCode);
        }

        private void SendSupplierOrderEmail (List<OrderItem> supplierOrderItems, Guid supplier_UID, int pinCode)
        {
            Supplier _Supplier = __SupplierRepository.Get(supplier_UID);

            StringBuilder _StringBuilder = new StringBuilder();

            _StringBuilder.Append("Hello, " + _Supplier.Name + "!\n\nYou have received the following order:\n\n");

            foreach (OrderItem _Item in supplierOrderItems)
            {
                _StringBuilder.Append(_Item.Quantity + " x " + _Item.ProductName + "\n");
            }

            _StringBuilder.Append("\nPlease use the following pin code when completing delivery: " + pinCode + "\n\nKind Regards, Future Fridges!");

            __EmailManager.SendEmail(new EmailData
            {
                Recipient = _Supplier.Email,
                Subject = SUPPLIER_ORDER_EMAIL_SUBJECT,
                Body = _StringBuilder.ToString()
            });
        }

        public void UpdateOrderItem (OrderItem orderItem)
        {
            __OrderRepository.UpdateOrderItem(orderItem);
        }
    }
}
