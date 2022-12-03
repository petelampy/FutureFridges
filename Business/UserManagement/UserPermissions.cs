namespace FutureFridges.Business.UserManagement
{
    public class UserPermissions
    {
        public int Id { get; set; }
        public Guid User_UID { get; set; }
        public bool AddStock { get; set; }
        public bool ManageHealthAndSafetyReport { get; set; }
        public bool ManageUser { get; set; }
        public bool RemoveStock { get; set; }
        public bool ViewStock { get; set; }
    }
}
