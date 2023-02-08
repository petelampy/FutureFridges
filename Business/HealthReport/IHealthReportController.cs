namespace FutureFridges.Business.HealthReport
{
    public interface IHealthReportController
    {
        void CreateHealthReport (string safetyOfficerEmail, DateTime date);
    }
}