namespace FutureFridges.Business.UserManagement
{
    public class UserPermissions
    {
        public bool AddStock { get; set; }
        public bool CreateOrder { get; set; }
        public int Id { get; set; }
        public bool ManageHealthAndSafetyReport { get; set; }
        public bool ManageOrders { get; set; }
        public bool ManageProduct { get; set; }
        public bool ManageSuppliers { get; set; }
        public bool ManageUser { get; set; }
        public bool RemoveStock { get; set; }
        public Guid User_UID { get; set; }
        public bool ViewAuditLog { get; set; }
        public bool ViewStock { get; set; }
    }
}
