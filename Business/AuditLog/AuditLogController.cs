using FutureFridges.Business.Enums;
using FutureFridges.Data.AuditLog;

namespace FutureFridges.Business.AuditLog
{
    public class AuditLogController : IAuditLogController
    {
        private readonly IAuditLogRepository __AuditLogRepository;

        public AuditLogController ()
            : this(new AuditLogRepository())
        { }

        internal AuditLogController (IAuditLogRepository auditLogRepository)
        {
            __AuditLogRepository = auditLogRepository;
        }


        public void Create (string name, string description, LogType logType)
        {
            LogEntry _LogEntry = new LogEntry
            {
                UserSupplierName = name,
                Description = description,
                LogType = logType,
                EventTime = DateTime.Now,
                UID = Guid.NewGuid()
            };

            __AuditLogRepository.Create(_LogEntry);
        }

        public List<LogEntry> GetAll ()
        {
            return __AuditLogRepository.GetAll();
        }

        public List<LogEntry> GetStockLogs ()
        {
            return __AuditLogRepository.GetStockLogs();
        }
    }
}
