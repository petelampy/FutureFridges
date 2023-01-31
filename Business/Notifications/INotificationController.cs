namespace FutureFridges.Business.Notifications
{
    public interface INotificationController
    {
        void CreateNotifications ();
        void CreateProductNotifications ();
        void Delete (Guid uid);
        List<Notification> GetByUser (Guid userUID);
        int GetCountByUser (Guid userUID);
    }
}