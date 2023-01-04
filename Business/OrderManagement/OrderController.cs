using FutureFridges.Data.OrderManagement;
using FutureFridges.Data.StockManagement;

namespace FutureFridges.Business.OrderManagement
{
    public class OrderController : IOrderController
    {
        private readonly IOrderRepository __OrderRepository;
        private readonly IProductRepository __ProductRepository;

        public OrderController ()
            : this(new OrderRepository(), new ProductRepository())
        { }

        internal OrderController (IOrderRepository orderRepository, IProductRepository productRepository)
        {
            __OrderRepository = orderRepository;
            __ProductRepository = productRepository;
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

        public bool IsValidOrderPinCode (int pinCode)
        {
            return __OrderRepository.IsValidOrderPinCode(pinCode);
        }

        //CREATE PIN CODE

        //CREATE ORDER

        //CREATE ORDER ITEMS
    }
}
