using BillShare.Extensions;
using BillShare.Requests.Groups;
using Contracts.DTOs.General;
using Contracts.DTOs.Groups;
using Contracts.Responses.General;
using Contracts.Responses.Groups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace BillShare.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public GroupsController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<GroupResponse>> CreateGroup([FromBody] CreateGroupRequest request)
    {
        var dto = new CreateGroupDto
        {
            CreatorId = User.GetUserId(),
            GroupName = request.GroupName,
            Participants = request.Participants
        };
        var response = await _serviceManager.GroupService.CreateGroupAsync(dto);
        return CreatedAtAction("GetGroup", new
        {
            groupId = response.GroupId
        }, response);
    }

    [HttpGet]
    [Route("{groupId:guid}")]
    public async Task<ActionResult<GroupResponse>> GetGroup([FromRoute] Guid groupId)
    {
        var response = await _serviceManager.GroupService.GetGroupByIdAsync(groupId);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponse<GroupResponse>>> GetGroups([FromQuery] PaginationDto pagination)
    {
        var path = $"{Request.Host}{Request.Path}";
        var dto = new GetGroupsDto
        {
            Pagination = pagination,
            EndpointUrl = new Uri(path)
        };
        var response = await _serviceManager.GroupService.GetPagedGroupsAsync(dto);
        return Ok(response);
    }
}