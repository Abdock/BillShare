using AutoMapper;
using Contracts.DTOs.ExpenseItems;
using Contracts.DTOs.ExpenseParticipants;
using Contracts.DTOs.Expenses;
using Contracts.DTOs.General;
using Contracts.Responses.ExpenseParticipants;
using Contracts.Responses.Expenses;
using Contracts.Responses.General;
using Domain.Enums;
using Domain.Models;
using Domain.Repositories;
using Services.Abstractions;

namespace Services;

public class ExpenseService : IExpenseService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPaginationService _paginationService;

    public ExpenseService(IUnitOfWork unitOfWork, IMapper mapper, IPaginationService paginationService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _paginationService = paginationService;
    }

    public async Task<ExpenseResponse> CreateExpenseAsync(CreateExpenseDto dto,
        CancellationToken cancellationToken = default)
    {
        var expense = _mapper.Map<Expense>(dto);
        foreach (var participant in dto.ExpenseParticipants)
        {
            expense.ExpenseParticipants.Add(new ExpenseParticipant
            {
                CustomerId = participant.UserId,
                ExpenseId = expense.Id
            });
        }

        foreach (var multiplier in dto.ExpenseMultipliers)
        {
            expense.ExpenseMultipliers.Add(new ExpenseMultiplier
            {
                ExpenseId = expense.Id,
                Multiplier = multiplier.Multiplier,
                Name = multiplier.Name
            });
        }

        foreach (var item in dto.ExpenseItems)
        {
            expense.ExpenseItems.Add(new ExpenseItem
            {
                StatusId = ExpenseItemStatusId.Active,
                Amount = item.Amount,
                Name = item.Name,
                Count = item.Count,
                ExpenseId = expense.Id
            });
        }

        await _unitOfWork.ExpenseRepository.AddExpenseAsync(expense, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _unitOfWork.ExpenseRepository.LoadRelatedDataAsync(expense, cancellationToken);
        var response = _mapper.Map<ExpenseResponse>(expense);
        foreach (var participant in response.Participants)
        {
            var path = dto.RemoveParticipantUrl.GetLeftPart(UriPartial.Path);
            participant.Actions = new ExpenseParticipantActionResponse
            {
                RemoveParticipantUrl = $"{path}/{participant.ParticipantId}/delete"
            };
        }

        return response;
    }

    public async Task<ExpenseResponse> GetExpenseByIdAsync(GetExpenseByIdDto dto,
        CancellationToken cancellationToken = default)
    {
        var expense = await _unitOfWork.ExpenseRepository
            .GetExpenseByIdAsync(dto.ExpenseId, dto.CustomerId, cancellationToken);
        await _unitOfWork.ExpenseRepository.LoadRelatedDataAsync(expense, cancellationToken);
        var response = _mapper.Map<ExpenseResponse>(expense);
        if (dto.CustomerId == response.CreatorId)
        {
            foreach (var participant in response.Participants)
            {
                var path = dto.RemoveParticipantUrl.GetLeftPart(UriPartial.Path);
                participant.Actions = new ExpenseParticipantActionResponse
                {
                    RemoveParticipantUrl = $"{path}/{participant.ParticipantId}/delete"
                };
            }
        }

        return response;
    }

    public async Task<PagedResponse<ExpenseResponse>> GetPagedExpensesAsync(GetUserExpensesDto dto,
        CancellationToken cancellationToken = default)
    {
        var skipCount = (dto.Pagination.PageNumber - 1) * dto.Pagination.PageSize;
        var expenses = await _unitOfWork.ExpenseRepository
            .GetPagedExpensesAsync(dto.UserId, skipCount, dto.Pagination.PageSize, cancellationToken);
        var totalCount = await _unitOfWork.ExpenseRepository.TotalCountAsync(dto.UserId, cancellationToken);
        var createPagination = new CreatePagedResponseDto<ExpenseResponse>
        {
            Responses = expenses.Select(e => _mapper.Map<ExpenseResponse>(e)),
            TotalCount = totalCount,
            EndpointUrl = dto.EndpointUrl.ToString(),
            CurrentPage = dto.Pagination
        };
        var pagedResponse = _paginationService.CreatePagedResponse(createPagination);
        return pagedResponse;
    }

    public async Task LockExpense(LockExpenseDto dto, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.ExpenseRepository.LockExpenseAsync(dto.UserId, dto.ExpenseId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UnlockExpenseDto(UnlockExpenseDto dto, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.ExpenseRepository.UnlockExpenseAsync(dto.UserId, dto.ExpenseId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task AddParticipantToExpenseAsync(AddExpenseParticipantDto dto,
        CancellationToken cancellationToken = default)
    {
        await _unitOfWork.ExpenseRepository.AddParticipantAsync(dto.ExpenseId, dto.UserId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveParticipantFromExpenseAsync(RemoveExpenseParticipantDto dto,
        CancellationToken cancellationToken = default)
    {
        await _unitOfWork.ExpenseRepository.DeleteParticipantAsync(dto.ExpenseId, dto.ParticipantId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task AddItemToExpenseAsync(AddExpenseItemDto dto, CancellationToken cancellationToken = default)
    {
        var item = new ExpenseItem
        {
            ExpenseId = dto.ExpenseId,
            Count = dto.Count,
            Name = dto.Name,
            StatusId = ExpenseItemStatusId.Active,
            Amount = dto.Amount
        };
        await _unitOfWork.ExpenseRepository.AddItemAsync(dto.ExpenseId, dto.CustomerId, item, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveItemFromExpenseAsync(RemoveExpenseItemDto dto, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.ExpenseRepository.DeleteItemAsync(dto.ExpenseId, dto.ExpenseItemId, dto.CustomerId,
            cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task SelectItemInExpenseAsync(SelectExpenseItemDto dto, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.ExpenseRepository.SelectItemAsync(dto.ExpenseId, dto.ExpenseItemId, dto.CustomerId,
            cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UnselectItemInExpenseAsync(UnselectExpenseItemDto dto,
        CancellationToken cancellationToken = default)
    {
        await _unitOfWork.ExpenseRepository.UnselectItemAsync(dto.ExpenseId, dto.ExpenseItemId, dto.CustomerId,
            cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}