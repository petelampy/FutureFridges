using FutureFridges.Business.StockManagement;
using FutureFridges.Data.OrderManagement;
using FutureFridges.Data.StockManagement;

namespace FutureFridges.Business.OrderManagement
{
    public class OrderController : IOrderController
    {
        private readonly IOrderRepository __OrderRepository;
        private readonly IProductRepository __ProductRepository; //REPLACE WITH CONTROLLER, FIND A WAY TO DEAL WITH THEM CALLING EACH OTHER

        public OrderController ()
            : this(new OrderRepository(), new ProductRepository())
        { }

        internal OrderController (IOrderRepository orderRepository, IProductRepository productRepository)
        {
            __OrderRepository = orderRepository;
            __ProductRepository = productRepository;
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

        private int CreatePinCode ()
        {
            List<int> _ValidPinCodes = Enumerable.Range(1000, 9999).ToList();

            List<int> _UsedPinCodes = __OrderRepository
                .GetAll()
                .Select(order => order.PinCode)
                .ToList();

            List<int> _AvailablePinCodes = _ValidPinCodes.Except(_UsedPinCodes).ToList();

            Random _Random = new Random();
            int _SelectedPinIndex = _Random.Next(_AvailablePinCodes.Count());

            return _AvailablePinCodes[_SelectedPinIndex];
        }

        public void CompleteOrder (Guid uid)
        {
            Order _Order = GetOrder(uid);

            List<Guid> _Supplier_UIDs = _Order.OrderItems
                .Select(orderItem => orderItem.Supplier_UID)
                .ToList();

            foreach (Guid _Supplier_UID in _Supplier_UIDs)
            {
                Order _SupplierOrder = new Order();
                _SupplierOrder.Supplier_UID = _Supplier_UID;
                CreateOrder(_SupplierOrder);

                List<OrderItem> _SupplierOrderItems = _Order.OrderItems
                    .Where(orderItem => orderItem.Supplier_UID == _Supplier_UID)
                    .ToList();

                foreach (OrderItem _Item in _SupplierOrderItems)
                {
                    CreateOrderItem(new OrderItem()
                    {
                        Order_UID = _SupplierOrder.UID,
                        ProductName = _Item.ProductName,
                        Product_UID = _Item.Product_UID,
                        Quantity = _Item.Quantity,
                        Supplier_UID = _Item.Supplier_UID
                    });
                }

                __OrderRepository.UpdateItemCount(_SupplierOrder.UID);
            }

            //EMAIL EACH SUPPLIER WITH MINI ORDERS

            //DELETE ORIGINAL ORDER ONCE NEW ONES ARE CREATED
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
            return __OrderRepository.GetAll();
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

        public bool IsValidOrderPinCode (int pinCode)
        {
            return __OrderRepository.IsValidOrderPinCode(pinCode);
        }

        public void UpdateOrderItem (OrderItem orderItem)
        {
            __OrderRepository.UpdateOrderItem(orderItem);
        }
    }
}
