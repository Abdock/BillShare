using Contracts.DTOs.Reports;

namespace Services.Abstractions;

public interface IReportService
{
    Task<Report> ReportForPeriod(Guid customerId, DateTime start, DateTime end,
        CancellationToken cancellationToken = default);
}