using BillShare.Extensions;
using BillShare.Requests.ExpenseItems;
using BillShare.Requests.ExpenseParticipants;
using BillShare.Requests.Expenses;
using Contracts.DTOs.ExpenseItems;
using Contracts.DTOs.ExpenseMultipliers;
using Contracts.DTOs.ExpenseParticipants;
using Contracts.DTOs.Expenses;
using Contracts.DTOs.General;
using Contracts.Responses.Expenses;
using Contracts.Responses.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace BillShare.Controllers;

[ApiController]
[Route("[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public ExpensesController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    #region Expenses
    
    [HttpGet]
    [Authorize]
    [Route("{expenseId:guid}")]
    public async Task<ActionResult<ExpenseResponse>> GetExpenseById([FromRoute] Guid expenseId)
    {
        var path = $"{Request.Host}{Request.Path}";
        var dto = new GetExpenseByIdDto
        {
            ExpenseId = expenseId,
            CustomerId = User.GetUserId(),
            RemoveParticipantUrl = new Uri(path)
        };
        var expense = await _serviceManager.ExpenseService.GetExpenseByIdAsync(dto);
        return Ok(expense);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ExpenseResponse>> CreateExpense([FromBody] CreateExpenseRequest request)
    {
        var path = $"{Request.Host}{Request.Path}";
        var dto = new CreateExpenseDto
        {
            CreatorId = User.GetUserId(),
            ExpenseTypeId = request.ExpenseTypeId,
            CategoryId = request.CategoryId,
            AccountId = request.AccountId,
            Amount = request.Amount,
            RemoveParticipantUrl = new Uri(path),
            ExpenseItems = request.Items.Select(item => new CreateExpenseItemDto
                {
                    Name = item.Name,
                    Count = item.Count,
                    Amount = (int) item.Amount
                })
                .ToList(),
            ExpenseMultipliers = request.Multipliers.Select(multiplier => new CreateExpenseMultiplierDto
                {
                    Name = multiplier.Name,
                    Multiplier = multiplier.CostMultiplierPercent * 0.01m
                })
                .ToList(),
            ExpenseParticipants = request.Participants.Select(participant => new AddNewExpenseParticipantDto
                {
                    UserId = participant.UserId
                })
                .ToList(),
            Name = request.Name
        };
        var expense = await _serviceManager.ExpenseService.CreateExpenseAsync(dto);
        return CreatedAtAction("GetExpenseById", new
        {
            expenseId = expense.Id
        }, expense);
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<PagedResponse<ExpenseResponse>>> GetExpenses([FromQuery] PaginationDto pagination)
    {
        var path = $"{Request.Host}{Request.Path}";
        var dto = new GetUserExpensesDto
        {
            UserId = User.GetUserId(),
            Pagination = pagination,
            EndpointUrl = new Uri(path)
        };
        var response = await _serviceManager.ExpenseService.GetPagedExpensesAsync(dto);
        return Ok(response);
    }

    [HttpPost]
    [Authorize]
    [Route("{expenseId:guid}/lock")]
    public async Task<ActionResult> LockExpense([FromRoute] Guid expenseId)
    {
        var dto = new LockExpenseDto
        {
            ExpenseId = expenseId,
            UserId = User.GetUserId()
        };
        await _serviceManager.ExpenseService.LockExpense(dto);
        return NoContent();
    }
    
    [HttpPost]
    [Authorize]
    [Route("{expenseId:guid}/unlock")]
    public async Task<ActionResult> UnlockExpense([FromRoute] Guid expenseId)
    {
        var dto = new UnlockExpenseDto
        {
            ExpenseId = expenseId,
            UserId = User.GetUserId()
        };
        await _serviceManager.ExpenseService.UnlockExpenseDto(dto);
        return NoContent();
    }
    
    #endregion
    
    #region Participants

    [HttpPost]
    [Authorize]
    [Route("{expenseId:guid}/participants")]
    public async Task<ActionResult> AddParticipantToExpense([FromRoute] Guid expenseId,
        [FromBody] AddExpenseParticipantRequest request)
    {
        var dto = new AddExpenseParticipantDto
        {
            UserId = User.GetUserId(),
            ExpenseId = expenseId
        };
        await _serviceManager.ExpenseService.AddParticipantToExpenseAsync(dto);
        return NoContent();
    }

    [HttpPost]
    [Authorize]
    [Route("{expenseId:guid}/participants/{participantId:guid}/delete")]
    public async Task<ActionResult> RemoveParticipantFromExpense([FromRoute] Guid expenseId,
        [FromRoute] Guid participantId)
    {
        var dto = new RemoveExpenseParticipantDto
        {
            ExpenseId = expenseId,
            ParticipantId = participantId
        };
        await _serviceManager.ExpenseService.RemoveParticipantFromExpenseAsync(dto);
        return NoContent();
    }
    
    #endregion
    
    #region Items

    [HttpPost]
    [Authorize]
    [Route("{expenseId:guid}/items")]
    public async Task<ActionResult> AddItemToExpense([FromRoute] Guid expenseId, [FromBody] AddExpenseItemRequest request)
    {
        var dto = new AddExpenseItemDto
        {
            ExpenseId = expenseId,
            CustomerId = User.GetUserId(),
            Name = request.Name,
            Count = request.Count,
            Amount = request.Amount
        };
        await _serviceManager.ExpenseService.AddItemToExpenseAsync(dto);
        return NoContent();
    }

    [HttpPost]
    [Authorize]
    [Route("{expenseId:guid}/items/{itemId:guid}/delete")]
    public async Task<ActionResult> RemoveItemFromExpense([FromRoute] Guid expenseId, [FromRoute] Guid itemId)
    {
        var dto = new RemoveExpenseItemDto
        {
            ExpenseId = expenseId,
            ExpenseItemId = itemId,
            CustomerId = User.GetUserId()
        };
        await _serviceManager.ExpenseService.RemoveItemFromExpenseAsync(dto);
        return NoContent();
    }

    [HttpPost]
    [Authorize]
    [Route("{expenseId:guid}/items/{itemId:guid}/select")]
    public async Task<ActionResult> SelectItemInExpense([FromRoute] Guid expenseId, Guid itemId)
    {
        var dto = new SelectExpenseItemDto
        {
            ExpenseId = expenseId,
            ExpenseItemId = itemId,
            CustomerId = User.GetUserId()
        };
        await _serviceManager.ExpenseService.SelectItemInExpenseAsync(dto);
        return NoContent();
    }

    [HttpPost]
    [Authorize]
    [Route("{expenseId:guid}/items/{itemId:guid}/unselect")]
    public async Task<ActionResult> UnselectItemInExpense([FromRoute] Guid expenseId, Guid itemId)
    {
        var dto = new UnselectExpenseItemDto
        {
            ExpenseId = expenseId,
            ExpenseItemId = itemId,
            CustomerId = User.GetUserId()
        };
        await _serviceManager.ExpenseService.UnselectItemInExpenseAsync(dto);
        return NoContent();
    }

    #endregion
}