using Contracts.DTOs.Icons;
using Contracts.Responses.Icons;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace BillShare.Controllers;

[ApiController]
[Route("[controller]")]
public class IconsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public IconsController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost]
    public async Task<ActionResult<IconResponse>> CreateIcon([FromBody] CreateIconDto icon)
    {
        var response = await _serviceManager.IconService.CreateIconAsync(icon);
        return CreatedAtAction("GetIconById", new
        {
            iconId = response.IconId
        }, response);
    }

    [HttpGet]
    [Route("{iconId:guid}")]
    public async Task<ActionResult<IconResponse>> GetIconById([FromRoute] Guid iconId)
    {
        var response = await _serviceManager.IconService.GetIconByIdAsync(iconId);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IconResponse>>> GetAllIcons()
    {
        var response = await _serviceManager.IconService.GetAllIconsAsync();
        return Ok(response);
    }
}