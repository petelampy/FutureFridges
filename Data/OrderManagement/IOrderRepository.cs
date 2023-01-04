using FutureFridges.Business.OrderManagement;

namespace FutureFridges.Data.OrderManagement
{
    public interface IOrderRepository
    {
        List<Order> GetAll ();
        Order GetOrder (Guid uid);
        Order GetOrderByPinCode (int pinCode);
        bool IsValidOrderPinCode (int pinCode);
    }
}