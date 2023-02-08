using FutureFridges.Business.AuditLog;
using FutureFridges.Business.Email;
using FutureFridges.Business.Enums;
using FutureFridges.Data.AuditLog;

namespace FutureFridgesTest.AuditLog
{
    [TestClass]
    public class AuditLogControllerTest
    {
        [TestMethod]
        public void AuditLogController_Create_ReturnsValidAuditLogEntry ()
        {
            Mock<IAuditLogRepository> _MockRepository = new Mock<IAuditLogRepository>();

            LogEntry _Result = new LogEntry();

            _MockRepository.Setup(mock => mock.Create(It.IsAny<LogEntry>()))
                .Callback((LogEntry entry) => { _Result = entry; })
                .Verifiable();

            IAuditLogController _AuditLogController = new AuditLogController(_MockRepository.Object);

            string _MockAuditLogName = "Log Entry Name";
            string _MockAuditLogDescription = "This is a log entry";
            
            _AuditLogController.Create(_MockAuditLogName, _MockAuditLogDescription, LogType.UserUpdate);

            Assert.AreNotEqual(Guid.Empty, _Result.UID);
            
            Assert.AreEqual(_MockAuditLogName, _Result.UserSupplierName);
            Assert.AreEqual(_MockAuditLogDescription, _Result.Description);
            Assert.AreEqual(LogType.UserUpdate, _Result.LogType);

            _MockRepository.Verify(repo => repo.Create(It.IsAny<LogEntry>()), Times.Once());
        }
    }
}
