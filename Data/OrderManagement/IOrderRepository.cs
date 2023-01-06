using FutureFridges.Business.OrderManagement;

namespace FutureFridges.Data.OrderManagement
{
    public interface IOrderRepository
    {
        void CreateOrder (Order newOrder);
        void CreateOrderItem (OrderItem orderItem);
        void DeleteOrder (Order order);
        void DeleteOrderItem (OrderItem orderItem);
        List<Order> GetAll ();
        Order GetOrder (Guid uid);
        Order GetOrderByPinCode (int pinCode);
        List<OrderItem> GetOrderItems (Guid order_UID);
        bool IsValidOrderPinCode (int pinCode);
        void UpdateOrderItem (OrderItem orderItem);
    }
}