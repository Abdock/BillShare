using Contracts.Responses.ExpenseTypes;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace BillShare.Controllers;

[ApiController]
[Route("expense-types")]
public class ExpenseTypesController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public ExpenseTypesController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExpenseTypeResponse>>> GetAllExpenseTypesAsync()
    {
        var response = await _serviceManager.ExpenseTypeService.GetAllExpenseTypesAsync();
        return Ok(response);
    }
}