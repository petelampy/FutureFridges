using FutureFridges.Business.Admin;
using FutureFridges.Business.StockManagement;
using FutureFridges.Data.Notifications;

namespace FutureFridges.Business.Notifications
{
    public class NotificationController : INotificationController
    {
        private const string EXPIRY_FORMAT = "An expired {0} needs removing from the fridge";
        private const string LOW_STOCK_FORMAT = "There is low stock of {0}. An order needs placing";

        private readonly INotificationRepository __NotificationRepository;
        private readonly IProductController __ProductController;
        private readonly ISettingsController __SettingsController;
        private readonly IStockItemController __StockItemController;

        public NotificationController ()
            : this(new NotificationRepository(), new StockItemController(), new ProductController(), new SettingsController())
        { }

        internal NotificationController (INotificationRepository notificationRepository, IStockItemController stockItemController,
            IProductController productController, ISettingsController settingsController)
        {
            __NotificationRepository = notificationRepository;
            __StockItemController = stockItemController;
            __ProductController = productController;
            __SettingsController = settingsController;
        }

        private void CreateExpiryNotifications ()
        {
            List<Product> _Products = __ProductController.GetAll();
            List<StockItem> _StockItems = __StockItemController.GetAll();

            foreach (StockItem _StockItem in _StockItems)
            {
                bool _NotificationExists = __NotificationRepository
                    .GetAll()
                    .Where(notification => notification.StockItem_UID == _StockItem.Item_UID)
                    .Count() > 0;

                if (_StockItem.ExpiryDate < DateTime.Now && !_NotificationExists)
                {

                    string _ProductName = _Products
                        .Where(product => product.UID == _StockItem.Product_UID)
                        .Select(product => product.Name)
                        .FirstOrDefault("");

                    Notification _Notification = new Notification()
                    {
                        UID = Guid.NewGuid(),
                        DateCreated = DateTime.Now,
                        Message = string.Format(EXPIRY_FORMAT, _ProductName),
                        User_UID = __SettingsController.Get().Administrator_UID,
                        StockItem_UID = _StockItem.Item_UID,
                    };

                    __NotificationRepository.Create(_Notification);
                }
            }
        }

        public void CreateNotifications ()
        {
            CreateProductNotifications();
            CreateExpiryNotifications();
        }

        public void CreateProductNotification (Guid product_UID)
        {
            Product _Product = __ProductController.GetProduct(product_UID);
            List<StockItem> _StockItems = __StockItemController.GetAll();

            bool _NotificationExists = __NotificationRepository
                    .GetAll()
                    .Where(notification => notification.Product_UID == _Product.UID)
                    .Count() > 0;

            int _QuantityInStock = _StockItems.Where(stockItem => stockItem.Product_UID == _Product.UID).Count();

            if (_QuantityInStock <= _Product.MinimumStockLevel && !_NotificationExists)
            {
                Notification _Notification = new Notification()
                {
                    UID = Guid.NewGuid(),
                    DateCreated = DateTime.Now,
                    Message = string.Format(LOW_STOCK_FORMAT, _Product.Name),
                    User_UID = __SettingsController.Get().Administrator_UID,
                    Product_UID = _Product.UID
                };

                __NotificationRepository.Create(_Notification);
            }
        }

        private void CreateProductNotifications ()
        {
            List<Product> _Products = __ProductController.GetAll();
            List<StockItem> _StockItems = __StockItemController.GetAll();

            foreach (Product _Product in _Products)
            {
                bool _NotificationExists = __NotificationRepository
                    .GetAll()
                    .Where(notification => notification.Product_UID == _Product.UID)
                    .Count() > 0;

                int _QuantityInStock = _StockItems.Where(stockItem => stockItem.Product_UID == _Product.UID).Count();

                if (_QuantityInStock <= _Product.MinimumStockLevel && !_NotificationExists)
                {
                    Notification _Notification = new Notification()
                    {
                        UID = Guid.NewGuid(),
                        DateCreated = DateTime.Now,
                        Message = string.Format(LOW_STOCK_FORMAT, _Product.Name),
                        User_UID = __SettingsController.Get().Administrator_UID,
                        Product_UID = _Product.UID
                    };

                    __NotificationRepository.Create(_Notification);
                }
            }
        }

        public void Delete (Guid uid)
        {
            __NotificationRepository.Delete(uid);
        }

        public List<Notification> GetByUser (Guid userUID)
        {
            return __NotificationRepository
                .GetAll()
                .Where(notification => notification.User_UID == userUID)
                .ToList();
        }

        public int GetCountByUser (Guid userUID)
        {
            return __NotificationRepository
                .GetAll()
                .Where(notification => notification.User_UID == userUID)
                .Count();
        }
    }
}
