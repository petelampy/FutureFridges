using System.ComponentModel.DataAnnotations.Schema;

namespace FutureFridges.Business.OrderManagement
{
    public class Order
    {
        public int Id { get; set; }
        public int NumberOfItems { get; set; }

        [NotMapped]
        public List<OrderItem>? OrderItems { get; set; }

        public int PinCode { get; set; }
        public Guid? Supplier_UID { get; set; }
        public Guid UID { get; set; }
    }
}
