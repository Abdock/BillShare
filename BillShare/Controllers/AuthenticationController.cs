using Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace BillShare.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public AuthenticationController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult<AuthenticationToken>> Register([FromBody] SignUpUserCredentials credentials)
    {
        var token = await _serviceManager.AuthenticationService.SignUpAsync(credentials);
        return Ok(token);
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<AuthenticationToken>> Login([FromBody] SignInUserCredentials credentials)
    {
        var token = await _serviceManager.AuthenticationService.SignInAsync(credentials);
        return Ok(token);
    }
}