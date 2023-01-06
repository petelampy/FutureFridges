﻿using FutureFridges.Business.OrderManagement;

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
            //SET THE RETURN AS A BOOL, THROW AN ERROR IF IT FAILS TO CREATE?

            __DbContext.Orders.Add(newOrder);
            __DbContext.SaveChanges();
        }

        public void CreateOrderItem (OrderItem orderItem)
        {
            //SET THE RETURN AS A BOOL, THROW AN ERROR IF IT FAILS TO CREATE?

            __DbContext.OrderItems.Add(orderItem);
            __DbContext.SaveChanges();
        }

        public void DeleteOrder (Order order)
        {
            __DbContext.Orders.Remove(order);
            __DbContext.SaveChanges();
        }

        public void DeleteOrderItem(OrderItem orderItem)
        {
            __DbContext.OrderItems.Remove(orderItem);
            __DbContext.SaveChanges();

            UpdateItemCount(orderItem.Order_UID);
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

        public List<OrderItem> GetOrderItems (Guid order_UID)
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

        public void UpdateItemCount(Guid orderUID)
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
