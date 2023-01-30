using FutureFridges.Business.Notifications;

namespace FutureFridges.Data.Notifications
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly FridgeDBContext __DbContext;
        private readonly IDbContextInitialiser __DbContextInitialiser;

        public NotificationRepository () :
            this(new DbContextInitialiser())
        { }

        internal NotificationRepository (IDbContextInitialiser dbContextInitialiser)
        {
            __DbContextInitialiser = dbContextInitialiser;
            __DbContext = __DbContextInitialiser.CreateNewDbContext();
        }

        public void Create (Notification notification)
        {
            __DbContext.Notifications.Add(notification);
            __DbContext.SaveChanges();
        }

        public List<Notification> GetAll () {
            return __DbContext.Notifications.ToList();
        }

        public Notification Get (Guid uid)
        {
            return __DbContext.Notifications
                .Where(notification => notification.UID == uid)
                .FirstOrDefault();
        }

        public void Delete (Guid uid)
        {
            Notification _CurrentNotification = Get(uid);
            
            __DbContext.Remove(_CurrentNotification);
            __DbContext.SaveChanges();
        }
    }
}
