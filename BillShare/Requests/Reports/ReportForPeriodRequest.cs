namespace BillShare.Requests.Reports;

public record ReportForPeriodRequest
{
    public required string StartDate { get; init; }
    public required string EndDate { get; init; }
}