using FutureFridges.Business.OrderManagement;

namespace FutureFridges.Data.OrderManagement
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FridgeDBContext __DbContext;
        private readonly IDbContextInitialiser __DbContextInitialiser;

        public OrderRepository () :
            this(new DbContextInitialiser())
        { }

        internal OrderRepository (IDbContextInitialiser dbContextInitialiser)
        {
            __DbContextInitialiser = dbContextInitialiser;
            __DbContext = __DbContextInitialiser.CreateNewDbContext();
        }

        public List<Order> GetAll ()
        {
            return __DbContext.Orders.ToList();
        }

        public Order GetOrder (Guid uid)
        {
            Order _Order = __DbContext.Orders
                .ToList()
                .Where(order => order.UID == uid)
                .SingleOrDefault(new Order());

            _Order.OrderItems = GetOrderItems(uid);

            return _Order;
        }

        public Order GetOrderByPinCode (int pinCode)
        {
            Order _Order = __DbContext.Orders
                .ToList()
                .Where(order => order.PinCode == pinCode)
                .SingleOrDefault(new Order());

            _Order.OrderItems = GetOrderItems(_Order.UID);

            return _Order;
        }

        private List<OrderItem> GetOrderItems (Guid order_UID)
        {
            return __DbContext
                .OrderItems
                .Where(orderItem => orderItem.Order_UID == order_UID)
                .ToList();
        }

        public bool IsValidOrderPinCode (int pinCode)
        {
            Order _Order = __DbContext.Orders
                .ToList()
                .Where(order => order.PinCode == pinCode)
                .SingleOrDefault(new Order());

            return _Order.UID != Guid.Empty;
        }
    }
}
