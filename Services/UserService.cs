using AutoMapper;
using Contracts.DTOs.Customers;
using Contracts.DTOs.General;
using Contracts.Responses.Customers;
using Contracts.Responses.General;
using Domain.Repositories;
using Services.Abstractions;

namespace Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPaginationService _paginationService;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, IPaginationService paginationService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _paginationService = paginationService;
    }

    public async Task<CustomerResponse> GetInformationAboutCustomerAsync(Guid customerId,
        CancellationToken cancellationToken = default)
    {
        var customer = await _unitOfWork.CustomerRepository.GetByCustomerIdAsync(customerId, cancellationToken);
        return _mapper.Map<CustomerResponse>(customer);
    }

    public async Task<PagedResponse<RelatedCustomerResponse>> SearchCustomersWithUsername(
        SearchCustomersByUsernameDto dto, CancellationToken cancellationToken = default)
    {
        var skipCount = (dto.Pagination.PageNumber - 1) * dto.Pagination.PageSize;
        var customers = await _unitOfWork.CustomerRepository
            .GetCustomersWithSameUsername(dto.Username, skipCount, dto.Pagination.PageSize, cancellationToken);
        var totalCount = await _unitOfWork.CustomerRepository
            .TotalCountOfCustomersWithUsernameAsync(dto.Username, cancellationToken);
        var url = dto.EndpointUrl.GetLeftPart(UriPartial.Path);
        var pagination = new CreatePagedResponseDto<RelatedCustomerResponse>
        {
            Responses = customers.Where(e => e.Id != dto.UserId).Select(e =>
            {
                var response = _mapper.Map<RelatedCustomerResponse>(e);
                response.IsFriend = e.Friends.Any(f => f.Id == dto.UserId);
                return response;
            }),
            TotalCount = totalCount,
            EndpointUrl = url,
            CurrentPage = dto.Pagination
        };
        return _paginationService.CreatePagedResponse(pagination);
    }
}