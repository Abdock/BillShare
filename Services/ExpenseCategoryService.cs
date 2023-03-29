using AutoMapper;
using Contracts.DTOs.ExpenseCategories;
using Contracts.Responses.ExpenseCategories;
using Domain.Models;
using Domain.Repositories;
using Services.Abstractions;

namespace Services;

public class ExpenseCategoryService : IExpenseCategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ExpenseCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ExpenseCategoryResponse> AddExpenseCategoryAsync(CreateExpenseCategoryDto dto,
        CancellationToken cancellationToken = default)
    {
        var category = _mapper.Map<CustomExpenseCategory>(dto);
        await _unitOfWork.CustomExpenseCategoriesRepository.AddExpenseCategoryAsync(category, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        category.Icon = await _unitOfWork.IconRepository.GetIconAsync(dto.IconId, cancellationToken);
        return _mapper.Map<ExpenseCategoryResponse>(category);
    }

    public async Task<IEnumerable<ExpenseCategoryResponse>> GetAllUserCategoriesAsync(Guid customerId,
        CancellationToken cancellationToken = default)
    {
        var expenseCategories = await _unitOfWork.CustomExpenseCategoriesRepository
            .GetAllCustomerExpenseCategories(customerId, cancellationToken);
        return expenseCategories.Select(category => _mapper.Map<ExpenseCategoryResponse>(category));
    }

    public async Task UpdateExpenseCategoryAsync(UpdateExpenseCategoryDto dto,
        CancellationToken cancellationToken = default)
    {
        await _unitOfWork.CustomExpenseCategoriesRepository.ChangeExpenseCategoryNameAsync(dto.UserId, dto.CategoryId, dto.Name,
            cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<ExpenseCategoryResponse> GetExpenseCategoryByIdAsync(Guid categoryId,
        CancellationToken cancellationToken = default)
    {
        var expenseCategory = await _unitOfWork.CustomExpenseCategoriesRepository
            .GetExpenseCategoryByIdAsync(categoryId, cancellationToken);
        return _mapper.Map<ExpenseCategoryResponse>(expenseCategory);
    }
}