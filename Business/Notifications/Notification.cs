namespace FutureFridges.Business.Notifications
{
    public class Notification
    {
        public DateTime DateCreated { get; set; }
        public int Id { get; set; }
        public string Message { get; set; }
        public Guid? Product_UID { get; set; }
        public Guid? StockItem_UID { get; set; }
        public Guid UID { get; set; }
        public Guid User_UID { get; set; }
    }
}
