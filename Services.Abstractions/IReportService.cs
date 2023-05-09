using Contracts.DTOs.Reports;

namespace Services.Abstractions;

public interface IReportService
{
    Task<Report> ReportForPeriodAsync(Guid customerId, DateTime start, DateTime end,
        CancellationToken cancellationToken = default);

    Task<Report> ReportSharedWithUserAsync(Guid requestSenderId, Guid userId,
        CancellationToken cancellationToken = default);
}