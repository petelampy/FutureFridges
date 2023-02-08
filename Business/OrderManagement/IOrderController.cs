namespace FutureFridges.Business.OrderManagement
{
    public interface IOrderController
    {
        void ClearUnfinishedOrders ();
        void CompleteOrder (Guid uid);
        void CreateOrder (Order order);
        void CreateOrderItem (OrderItem orderItem);
        void DeleteOrder (Guid uid);
        void DeleteOrderItem (Guid uid);
        List<Order> GetAll ();
        Order GetOrder (Guid uid);
        Order GetOrderByPinCode (int pinCode);
        List<OrderItem> GetOrderItems (Guid order_uid);
        List<OrderItem> GetOrderItemsByProduct (Guid product_UID);
        List<Order> GetOrdersBySupplier (Guid supplier_UID);
        bool IsValidOrderPinCode (int pinCode);
        void UpdateOrderItem (OrderItem orderItem);
    }
}