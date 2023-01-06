using FutureFridges.Data.OrderManagement;
using FutureFridges.Data.StockManagement;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Runtime.Intrinsics.Arm;

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

        public List<OrderItem> GetOrderItems (Guid order_uid)
        {
            List<OrderItem> _OrderItems = __OrderRepository.GetOrderItems(order_uid);

            _OrderItems = GetItemProductNames(_OrderItems);

            return _OrderItems;
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

        private int CreatePinCode()
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

        public void CreateOrder(Order order)
        {
            order.UID = Guid.NewGuid();
            order.PinCode = CreatePinCode();
            order.NumberOfItems = order.OrderItems.Select(orderItem => orderItem.Quantity).Sum();

            __OrderRepository.CreateOrder(order);
        }

        public void CreateOrderItem(OrderItem orderItem)
        {
            orderItem.UID = Guid.NewGuid();

            __OrderRepository.CreateOrderItem(orderItem);
        }

        public void DeleteOrder (Order order)
        {
            __OrderRepository.DeleteOrder(order);
        }

        public void DeleteOrderItem(Guid uid) { 
            __OrderRepository.DeleteOrderItem(uid);
        }

        public void UpdateOrderItem(OrderItem orderItem)
        {
            __OrderRepository.UpdateOrderItem(orderItem);
        }
    }
}
