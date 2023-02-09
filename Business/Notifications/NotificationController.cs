using FutureFridges.Business.Admin;
using FutureFridges.Business.Enums;
using FutureFridges.Business.OrderManagement;
using FutureFridges.Business.StockManagement;
using FutureFridges.Business.UserManagement;
using FutureFridges.Data.Notifications;
using Microsoft.AspNetCore.Identity;

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
        private readonly IOrderController __OrderController;
        private readonly IUserController __UserController;

        public NotificationController (UserManager<FridgeUser> userManager)
            : this(new NotificationRepository(), new StockItemController(), new ProductController(), new SettingsController(), new OrderController(), new UserController(userManager))
        { }

        internal NotificationController (INotificationRepository notificationRepository, IStockItemController stockItemController,
            IProductController productController, ISettingsController settingsController, IOrderController orderController, IUserController userController)
        {
            __NotificationRepository = notificationRepository;
            __StockItemController = stockItemController;
            __ProductController = productController;
            __SettingsController = settingsController;
            __OrderController = orderController;
            __UserController = userController;
        }

        private void CreateExpiryNotifications ()
        {
            List<Product> _Products = __ProductController.GetAll();
            List<StockItem> _StockItems = __StockItemController.GetAll();
            Settings _Settings = __SettingsController.Get();

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

                    if(_Settings.NotifyAllHeadChefs)
                    {
                        List<FridgeUser> _HeadChefs = __UserController
                            .GetAll()
                            .Where(user => user.UserType == UserType.HeadChef || user.UserType == UserType.Administrator)
                            .ToList();

                        foreach (FridgeUser _HeadChef in _HeadChefs)
                        {
                            __NotificationRepository.Create(new Notification
                            {
                                UID = Guid.NewGuid(),
                                DateCreated = DateTime.Now,
                                Message = string.Format(EXPIRY_FORMAT, _ProductName),
                                User_UID = new Guid(_HeadChef.Id),
                                StockItem_UID = _StockItem.Item_UID,
                            });
                        }
                    }
                    else
                    {
                        __NotificationRepository.Create(new Notification
                        {
                            UID = Guid.NewGuid(),
                            DateCreated = DateTime.Now,
                            Message = string.Format(EXPIRY_FORMAT, _ProductName),
                            User_UID = _Settings.Administrator_UID,
                            StockItem_UID = _StockItem.Item_UID,
                        });
                    }
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
            List<Order> _Orders = __OrderController.GetAll();
            Settings _Settings = __SettingsController.Get();

            bool _NotificationExists = __NotificationRepository
                    .GetAll()
                    .Where(notification => notification.Product_UID == _Product.UID)
                    .Count() > 0;

            int _QuantityInStock = _StockItems.Where(stockItem => stockItem.Product_UID == _Product.UID).Count();

            int _QuantityOnOrder = 0;
            foreach (Order _Order in _Orders)
            {
                _QuantityOnOrder += _Order.OrderItems
                    .Where(orderItem => orderItem.Product_UID == _Product.UID)
                    .Sum(orderItem => orderItem.Quantity);
            }

            if (_QuantityInStock + _QuantityOnOrder <= _Product.MinimumStockLevel && !_NotificationExists)
            {

                if (_Settings.NotifyAllHeadChefs)
                {
                    List<FridgeUser> _HeadChefs = __UserController
                        .GetAll()
                        .Where(user => user.UserType == UserType.HeadChef || user.UserType == UserType.Administrator)
                        .ToList();

                    foreach (FridgeUser _HeadChef in _HeadChefs)
                    {
                        __NotificationRepository.Create(new Notification
                        {
                            UID = Guid.NewGuid(),
                            DateCreated = DateTime.Now,
                            Message = string.Format(LOW_STOCK_FORMAT, _Product.Name),
                            User_UID = new Guid(_HeadChef.Id),
                            Product_UID = _Product.UID
                        });
                    }
                }
                else
                {
                    __NotificationRepository.Create(new Notification
                    {
                        UID = Guid.NewGuid(),
                        DateCreated = DateTime.Now,
                        Message = string.Format(LOW_STOCK_FORMAT, _Product.Name),
                        User_UID = _Settings.Administrator_UID,
                        Product_UID = _Product.UID
                    });
                }
            }
        }

        private void CreateProductNotifications ()
        {
            List<Product> _Products = __ProductController.GetAll();
            List<StockItem> _StockItems = __StockItemController.GetAll();
            List<Order> _Orders = __OrderController.GetAll();
            Settings _Settings = __SettingsController.Get();

            foreach (Product _Product in _Products)
            {
                bool _NotificationExists = __NotificationRepository
                    .GetAll()
                    .Where(notification => notification.Product_UID == _Product.UID)
                    .Count() > 0;

                int _QuantityInStock = _StockItems.Where(stockItem => stockItem.Product_UID == _Product.UID).Count();

                int _QuantityOnOrder = 0;
                foreach(Order _Order in _Orders)
                {
                    _QuantityOnOrder += _Order.OrderItems
                        .Where(orderItem => orderItem.Product_UID == _Product.UID)
                        .Sum(orderItem => orderItem.Quantity);
                }

                if (_QuantityInStock + _QuantityOnOrder <= _Product.MinimumStockLevel && !_NotificationExists)
                {
                    if (_Settings.NotifyAllHeadChefs)
                    {
                        List<FridgeUser> _HeadChefs = __UserController
                            .GetAll()
                            .Where(user => user.UserType == UserType.HeadChef || user.UserType == UserType.Administrator)
                            .ToList();

                        foreach (FridgeUser _HeadChef in _HeadChefs)
                        {
                            __NotificationRepository.Create(new Notification
                            {
                                UID = Guid.NewGuid(),
                                DateCreated = DateTime.Now,
                                Message = string.Format(LOW_STOCK_FORMAT, _Product.Name),
                                User_UID = new Guid(_HeadChef.Id),
                                Product_UID = _Product.UID
                            });
                        }
                    }
                    else
                    {
                        __NotificationRepository.Create(new Notification
                        {
                            UID = Guid.NewGuid(),
                            DateCreated = DateTime.Now,
                            Message = string.Format(LOW_STOCK_FORMAT, _Product.Name),
                            User_UID = _Settings.Administrator_UID,
                            Product_UID = _Product.UID
                        });
                    }
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
