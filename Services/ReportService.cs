using AutoMapper;
using Contracts.DTOs.Reports;
using Domain.Repositories;
using Services.Abstractions;

namespace Services;

public class ReportService : IReportService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ReportService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Report> ReportForPeriodAsync(Guid customerId, DateTime start, DateTime end,
        CancellationToken cancellationToken = default)
    {
        var spends = await _unitOfWork.CustomerRepository.GetSpendsForPeriodAsync(customerId, start, end, cancellationToken);
        return new Report
        {
            TotalSpendings = spends.Sum(e => e.TotalSpend),
            ExpenseCount = spends.Sum(spend => spend.ExpensesCount),
            CategoriesSpendings = spends.Select(s => _mapper.Map<CategorySpend>(s)).ToList()
        };
    }

    public async Task<Report> ReportSharedWithUserAsync(Guid requestSenderId, Guid userId,
        CancellationToken cancellationToken = default)
    {
        var spends = await _unitOfWork.CustomerRepository
            .GetSpendsSharedBetweenUsersAsync(requestSenderId, userId, cancellationToken);
        return new Report
        {
            TotalSpendings = spends.Sum(e => e.TotalSpend),
            ExpenseCount = spends.Sum(spend => spend.ExpensesCount),
            CategoriesSpendings = spends.Select(s => _mapper.Map<CategorySpend>(s)).ToList()
        };
    }
}