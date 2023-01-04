namespace FutureFridges.Business.OrderManagement
{
    public interface IOrderController
    {
        List<Order> GetAll ();
        Order GetOrder (Guid uid);
        Order GetOrderByPinCode (int pinCode);
        bool IsValidOrderPinCode (int pinCode);
    }
}