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

        public void CreateOrder (Order newOrder)
        {
            __DbContext.Orders.Add(newOrder);
            __DbContext.SaveChanges();
        }

        public void CreateOrderItem (OrderItem orderItem)
        {
            __DbContext.OrderItems.Add(orderItem);
            __DbContext.SaveChanges();

            UpdateItemCount(orderItem.Order_UID);
        }

        public void DeleteOrder (Guid uid)
        {
            Order _Order = GetOrder(uid);

            __DbContext.Orders.Remove(_Order);
            __DbContext.OrderItems.RemoveRange(_Order.OrderItems);
            __DbContext.SaveChanges();
        }

        public void DeleteOrderItem (Guid uid)
        {
            OrderItem _OrderItem = GetOrderItem(uid);

            __DbContext.OrderItems.Remove(_OrderItem);
            __DbContext.SaveChanges();

            UpdateItemCount(_OrderItem.Order_UID);
        }

        public List<Order> GetAll ()
        {
            List<Order> _Orders = __DbContext.Orders.ToList();

            foreach(Order _Order in _Orders)
            {
                _Order.OrderItems = GetOrderItems(_Order.UID);
            }

            return _Orders;
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

        public OrderItem GetOrderItem (Guid uid)
        {
            return __DbContext
                .OrderItems
                .AsEnumerable()
                .Where(orderItem => orderItem.UID == uid)
                .SingleOrDefault(new OrderItem());
        }

        public List<OrderItem> GetOrderItems (Guid order_UID)
        {
            return __DbContext
                .OrderItems
                .Where(orderItem => orderItem.Order_UID == order_UID)
                .ToList();
        }

        public List<OrderItem> GetOrderItemsByProduct (Guid product_UID)
        {
            return __DbContext
                .OrderItems
                .Where(orderItem => orderItem.Product_UID == product_UID)
                .ToList();
        }

        public List<Order> GetOrdersBySupplier (Guid supplier_UID)
        {
            return __DbContext
                .Orders
                .Where(order => order.Supplier_UID == supplier_UID)
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

        public void UpdateItemCount (Guid orderUID)
        {
            Order _Order = GetOrder(orderUID);

            _Order.NumberOfItems = _Order.OrderItems.Select(orderItem => orderItem.Quantity).Sum();

            __DbContext.SaveChanges();
        }

        public void UpdateOrderItem (OrderItem orderItem)
        {
            __DbContext.OrderItems.Update(orderItem);
            __DbContext.SaveChanges();

            UpdateItemCount(orderItem.Order_UID);
        }
    }
}
