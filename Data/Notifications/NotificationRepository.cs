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
    }
}
