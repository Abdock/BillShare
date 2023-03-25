using AutoMapper;
using Contracts.Responses.ExpenseTypes;
using Domain.Repositories;
using Services.Abstractions;

namespace Services;

public class ExpenseTypeService : IExpenseTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ExpenseTypeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ExpenseTypeResponse>> GetAllExpenseTypesAsync(CancellationToken cancellationToken = default)
    {
        var response = await _unitOfWork.ExpenseTypeRepository.GetAllExpenseTypesAsync(cancellationToken);
        return response.Select(type => _mapper.Map<ExpenseTypeResponse>(type));
    }
}