using Contracts.DTOs.General;
using Contracts.Responses.General;
using Services.Abstractions;

namespace Services;

public class PaginationService : IPaginationService
{
    public PagedResponse<TResponse> CreatePagedResponse<TResponse>(CreatePagedResponseDto<TResponse> dto)
    {
        var url = new Uri(dto.EndpointUrl);
        var baseUrl = url.GetLeftPart(UriPartial.Path);
        var firstPage = dto.CurrentPage.FirstPage();
        var lastPage = dto.CurrentPage.LastPage(dto.TotalCount);
        var nextPage = dto.CurrentPage.MoveNext();
        return new PagedResponse<TResponse>
        {
            TotalCount = dto.TotalCount,
            FirstPageUrl = $"{baseUrl}?{firstPage.GenerateQuery()}",
            LastPageUrl = $"{baseUrl}?{lastPage.GenerateQuery()}",
            NextPageUrl = $"{baseUrl}?{nextPage.GenerateQuery()}",
            Data = dto.Responses.ToList()
        };
    }
}