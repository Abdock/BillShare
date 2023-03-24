using Contracts.DTOs.General;
using Contracts.Responses.General;

namespace Services.Abstractions;

public interface IPaginationService
{
    PagedResponse<TResponse> CreatePagedResponse<TResponse>(CreatePagedResponseDto<TResponse> dto);
}