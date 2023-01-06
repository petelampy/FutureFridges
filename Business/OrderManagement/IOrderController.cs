namespace FutureFridges.Business.OrderManagement
{
    public interface IOrderController
    {
        void CreateOrder (Order order);
        void CreateOrderItem (OrderItem orderItem);
        void DeleteOrder (Order order);
        void DeleteOrderItem (Guid uid);
        List<Order> GetAll ();
        Order GetOrder (Guid uid);
        Order GetOrderByPinCode (int pinCode);
        List<OrderItem> GetOrderItems (Guid order_uid);
        bool IsValidOrderPinCode (int pinCode);
        void UpdateOrderItem (OrderItem orderItem);
    }
}