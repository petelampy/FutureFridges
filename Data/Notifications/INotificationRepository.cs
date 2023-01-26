using FutureFridges.Business.Notifications;

namespace FutureFridges.Data.Notifications
{
    public interface INotificationRepository
    {
        void Create (Notification notification);
        List<Notification> GetAll ();
    }
}