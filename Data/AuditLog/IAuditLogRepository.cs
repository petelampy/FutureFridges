using FutureFridges.Business.AuditLog;

namespace FutureFridges.Data.AuditLog
{
    public interface IAuditLogRepository
    {
        void Create (LogEntry logEntry);
        List<LogEntry> GetAll ();
        List<LogEntry> GetStockLogs ();
    }
}