namespace Contracts.DTOs.Reports;

public record CategorySpend
{
    public required Guid CategoryId { get; init; }
    public required string CategoryName { get; init; }
    public required decimal Total { get; init; }
}