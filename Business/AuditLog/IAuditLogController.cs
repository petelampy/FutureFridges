using FutureFridges.Business.Enums;

namespace FutureFridges.Business.AuditLog
{
    public interface IAuditLogController
    {
        void Create (string name, string description, LogType logType);
        List<LogEntry> GetAll ();
        List<LogEntry> GetStockLogs ();
    }
}