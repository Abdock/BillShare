using BillShare.Extensions;
using BillShare.Requests.ExpenseCategories;
using Contracts.DTOs.ExpenseCategories;
using Contracts.Responses.ExpenseCategories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace BillShare.Controllers;

[ApiController]
[Route("expense-categories")]
public class ExpenseCategoriesController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public ExpenseCategoriesController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ExpenseCategoryResponse>> CreateCategoryResponse(
        [FromBody] CreateExpenseCategoryRequest request)
    {
        var dto = new CreateExpenseCategoryDto
        {
            UserId = User.GetUserId(),
            CategoryName = request.CategoryName,
            IconId = request.IconId,
        };
        var response = await _serviceManager.ExpenseCategoryService.AddExpenseCategoryAsync(dto);
        return CreatedAtAction("GetExpenseCategoryById", new
        {
            categoryId = response.Id
        }, response);
    }

    [HttpGet]
    [Authorize]
    [Route("{categoryId:guid}")]
    public async Task<ActionResult<ExpenseCategoryResponse>> GetExpenseCategoryById([FromRoute] Guid categoryId)
    {
        var response = await _serviceManager.ExpenseCategoryService.GetExpenseCategoryByIdAsync(categoryId);
        return Ok(response);
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<ExpenseCategoryResponse>>> GetExpenseCategories()
    {
        var response = await _serviceManager.ExpenseCategoryService
            .GetAllUserCategoriesAsync(User.GetUserId());
        return Ok(response);
    }

    [HttpPatch]
    [Authorize]
    [Route("{expenseCategoryId:guid}")]
    public async Task<ActionResult> UpdateExpenseCategory([FromRoute] Guid expenseCategoryId,
        [FromBody] UpdateExpenseCategoryRequest request)
    {
        var dto = new UpdateExpenseCategoryDto
        {
            CategoryId = expenseCategoryId,
            UserId = User.GetUserId(),
            Name = request.Name
        };
        await _serviceManager.ExpenseCategoryService.UpdateExpenseCategoryAsync(dto);
        return NoContent();
    }
}