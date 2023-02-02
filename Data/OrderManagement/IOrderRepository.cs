using FutureFridges.Business.OrderManagement;

namespace FutureFridges.Data.OrderManagement
{
    public interface IOrderRepository
    {
        void CreateOrder (Order newOrder);
        void CreateOrderItem (OrderItem orderItem);
        void DeleteOrder (Guid uid);
        void DeleteOrderItem (Guid uid);
        List<Order> GetAll ();
        Order GetOrder (Guid uid);
        Order GetOrderByPinCode (int pinCode);
        OrderItem GetOrderItem (Guid uid);
        List<OrderItem> GetOrderItems (Guid order_UID);
        List<OrderItem> GetOrderItemsByProduct (Guid product_UID);
        List<Order> GetOrdersBySupplier (Guid supplier_UID);
        bool IsValidOrderPinCode (int pinCode);
        void UpdateItemCount (Guid orderUID);
        void UpdateOrderItem (OrderItem orderItem);
    }
}