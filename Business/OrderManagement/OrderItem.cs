using System.ComponentModel.DataAnnotations.Schema;

namespace FutureFridges.Business.OrderManagement
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Guid Order_UID { get; set; }
        public Guid Product_UID { get; set; }

        [NotMapped]
        public string? ProductName { get; set; }

        public int Quantity { get; set; }
        public Guid Supplier_UID { get; set; }
        public Guid UID { get; set; }
    }
}
