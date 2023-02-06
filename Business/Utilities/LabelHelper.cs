using FutureFridges.Business.Enums;

namespace FutureFridges.Business.Utilities
{
    public static class LabelHelper
    {
        public static string GetLabel (this LogType logtype)
        {
            switch (logtype)
            {
                case LogType.ItemTake:
                    return "Stock Item Taken";
                case LogType.ItemAdd:
                    return "Stock Item Added";
                case LogType.UserCreate:
                    return "User Created";
                case LogType.UserDelete:
                    return "User Deleted";
                case LogType.UserUpdate:
                    return "User Updated";
                case LogType.UserPasswordReset:
                    return "Password Reset";
                case LogType.ProductCreate:
                    return "Product Created";
                case LogType.ProductDelete:
                    return "Product Deleted";
                case LogType.ProductUpdate:
                    return "Product Updated";
                case LogType.SupplierCreate:
                    return "Supplier Created";
                case LogType.SupplierDelete:
                    return "Supplier Deleted";
                case LogType.SupplierUpdate:
                    return "Supplier Updated";
                case LogType.DeliveryReceive:
                    return "Delivery Received";
                case LogType.OrderCreate:
                    return "Order Created";
                default:
                    return "";
            }
        }
    }
}
