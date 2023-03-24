namespace Contracts.DTOs.General;

public record PaginationDto
{
    public required int PageNumber { get; init; }
    public required int PageSize { get; init; }

    public string GenerateQuery()
    {
        return $"{nameof(PageNumber)}={PageNumber}&{nameof(PageSize)}={PageSize}";
    }

    public PaginationDto MoveNext()
    {
        return this with {PageNumber = PageNumber + 1};
    }

    public PaginationDto FirstPage()
    {
        return this with {PageNumber = 1};
    }

    public PaginationDto LastPage(int totalCount)
    {
        var lastPage = (totalCount + PageSize - 1) / PageSize;
        return this with {PageNumber = lastPage};
    }
}