using FutureFridges.Business.AuditLog;
using FutureFridges.Business.Enums;

namespace FutureFridges.Data.AuditLog
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly FridgeDBContext __DbContext;
        private readonly IDbContextInitialiser __DbContextInitialiser;

        public AuditLogRepository () :
            this(new DbContextInitialiser())
        { }

        internal AuditLogRepository (IDbContextInitialiser dbContextInitialiser)
        {
            __DbContextInitialiser = dbContextInitialiser;
            __DbContext = __DbContextInitialiser.CreateNewDbContext();
        }

        public void Create (LogEntry logEntry)
        {
            __DbContext.AuditLogs.Add(logEntry);
            __DbContext.SaveChanges();
        }

        public List<LogEntry> GetAll ()
        {
            return __DbContext.AuditLogs.ToList();
        }

        public List<LogEntry> GetStockLogs ()
        {
            List<LogType> _StockLogTypes = new List<LogType> {
                LogType.ItemAdd,
                LogType.ItemTake,
                LogType.DeliveryReceive,
                LogType.OrderCreate
            };

            return __DbContext.AuditLogs
                .Where(logEntry => _StockLogTypes.Contains(logEntry.LogType))
                .ToList();
        }
    }
}
