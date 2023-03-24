namespace Contracts.Responses.General;

public class PagedResponse<TResponse>
{
    public required int TotalCount { get; init; }
    public required string FirstPageUrl { get; init; }
    public required string LastPageUrl { get; init; }
    public required string NextPageUrl { get; init; }
    public required ICollection<TResponse> Data { get; init; } = new List<TResponse>();
}