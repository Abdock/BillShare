namespace Contracts.DTOs.General;

public record CreatePagedResponseDto<TResponse>
{
    public required IEnumerable<TResponse> Responses { get; init; }
    public required int TotalCount { get; init; }
    public required string EndpointUrl { get; init; }
    public required PaginationDto CurrentPage { get; init; }
}