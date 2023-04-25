using BillShare.Extensions;
using Contracts.DTOs.Customers;
using Contracts.DTOs.General;
using Contracts.DTOs.Icons;
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

    [HttpPost]
    [Authorize]
    [Route("me/avatar")]
    public async Task<ActionResult<CustomerAvatarIcon>> ChangeAvatar([FromBody] CreateIconDto dto)
    {
        var serviceDto = new ChangeCustomerAvatarDto
        {
            CustomerId = User.GetUserId(),
            ImageData = dto.IconImageData,
            Extension = dto.Extension
        };
        var response = await _serviceManager.CustomerService.ChangeCustomerAvatarAsync(serviceDto);
        return Ok(response);
    }

    [HttpDelete]
    [Authorize]
    [Route("me/avatar")]
    public async Task<ActionResult> DeleteAvatar()
    {
        await _serviceManager.CustomerService.DeleteCustomerAvatarAsync(User.GetUserId());
        return NoContent();
    }

    [HttpGet]
    [Route("{userId:guid}")]
    public async Task<ActionResult<CustomerResponse>> GetCustomerById([FromRoute] Guid userId)
    {
        var customer = await _serviceManager.CustomerService.GetCustomerByIdAsync(userId);
        return Ok(customer);
    }
}