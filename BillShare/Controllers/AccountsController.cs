using BillShare.Extensions;
using BillShare.Requests.Accounts;
using Contracts.DTOs.Accounts;
using Contracts.Responses.Accounts;
using Contracts.Responses.Expenses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace BillShare.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public AccountsController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<AccountResponse>> CreateAccount([FromBody] CreateAccountRequest request)
    {
        var dto = new AddAccountDto
        {
            UserId = User.GetUserId(),
            Amount = request.Amount,
            Name = request.Name,
            ExternalId = request.ExternalId
        };
        var response = await _serviceManager.AccountService.AddAccountAsync(dto);
        return CreatedAtAction("GetAccountById", new
        {
            accountId = response.Id
        }, response);
    }

    [HttpGet]
    [Authorize]
    [Route("{accountId:guid}")]
    public async Task<ActionResult<AccountResponse>> GetAccountById([FromRoute] Guid accountId)
    {
        var dto = new GetUserAccountDto
        {
            UserId = User.GetUserId(),
            AccountId = accountId
        };
        var response = await _serviceManager.AccountService.GetAccountAsync(dto);
        return Ok(response);
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<AccountResponse>>> GetAccounts()
    {
        var dto = new GetUserAccountsDto
        {
            UserId = User.GetUserId()
        };
        var responses = await _serviceManager.AccountService.GetUserAccountsAsync(dto);
        return Ok(responses);
    }

    [HttpGet]
    [Authorize]
    [Route("{accountId:guid}/expenses")]
    public async Task<ActionResult<IEnumerable<ShortExpenseResponse>>> GetAccountPaidExpenses(
        [FromRoute] Guid accountId)
    {
        var dto = new GetPaidExpensesDto
        {
            UserId = User.GetUserId(),
            AccountId = accountId
        };
        var responses = await _serviceManager.AccountService.GetPaidExpensesByAccountAsync(dto);
        return Ok(responses);
    }

    [HttpPut]
    [Authorize]
    [Route("{accountId:guid}/disable")]
    public async Task<ActionResult> DisableAccount([FromRoute] Guid accountId)
    {
        var dto = new DisableAccountDto
        {
            UserId = User.GetUserId(),
            AccountId = accountId
        };
        await _serviceManager.AccountService.DisableAccountAsync(dto);
        return NoContent();
    }

    [HttpPut]
    [Authorize]
    [Route("{accountId:guid}/enable")]
    public async Task<ActionResult> EnableAccount([FromRoute] Guid accountId)
    {
        var dto = new EnableAccountDto
        {
            UserId = User.GetUserId(),
            AccountId = accountId
        };
        await _serviceManager.AccountService.EnableAccountAsync(dto);
        return NoContent();
    }

    [HttpPut]
    [Authorize]
    [Route("{accountId:guid}/amount")]
    public async Task<ActionResult> ChangeAccountAmount([FromRoute] Guid accountId,
        [FromBody] ChangeAccountAmountRequest request)
    {
        var dto = new ChangeAccountAmountDto
        {
            UserId = User.GetUserId(),
            AccountId = accountId,
            Amount = request.Amount
        };
        await _serviceManager.AccountService.ChangeAccountAmountAsync(dto);
        return NoContent();
    }
}