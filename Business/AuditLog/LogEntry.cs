using FutureFridges.Business.Enums;

namespace FutureFridges.Business.AuditLog
{
    public class LogEntry
    {
        public string Description { get; set; }
        public DateTime EventTime { get; set; }
        public int Id { get; set; }
        public LogType LogType { get; set; }
        public Guid UID { get; set; }
        public string? UserSupplierName { get; set; }
    }
}
