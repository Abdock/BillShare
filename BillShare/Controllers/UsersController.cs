using BillShare.Extensions;
using Contracts.DTOs.Customers;
using Contracts.DTOs.General;
using Contracts.Responses.Customers;
using Contracts.Responses.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace BillShare.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public UsersController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    [Authorize]
    [Route("me")]
    public async Task<ActionResult<CustomerResponse>> AboutMe()
    {
        var response = await _serviceManager.UserService.GetInformationAboutCustomerAsync(User.GetUserId());
        return Ok(response);
    }

    [HttpGet]
    [Authorize]
    [Route("search")]
    public async Task<ActionResult<PagedResponse<RelatedCustomerResponse>>> SearchUser([FromQuery] string username,
        [FromQuery] PaginationDto pagination)
    {
        var path = new Uri($"{Request.Host}{Request.Path.Value!}");
        var dto = new SearchCustomersByUsernameDto
        {
            UserId = User.GetUserId(),
            Username = username,
            Pagination = pagination,
            EndpointUrl = path
        };
        var response = await _serviceManager.UserService.SearchCustomersWithUsername(dto);
        return Ok(response);
    }
}