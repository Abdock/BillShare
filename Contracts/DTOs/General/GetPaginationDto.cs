namespace Contracts.DTOs.General;

public record GetPaginationDto
{
    public required PaginationDto Pagination { get; init; }
    public required Uri EndpointUrl { get; init; }
}