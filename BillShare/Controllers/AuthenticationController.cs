using Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions.Authentication;

namespace BillShare.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IJwtAuthenticationService _authenticationService;

    public AuthenticationController(IJwtAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult<AuthenticationToken>> Register([FromBody] SignUpUserCredentials credentials)
    {
        var token = await _authenticationService.SignUpAsync(credentials);
        return Ok(token);
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult<AuthenticationToken>> Login([FromBody] SignInUserCredentials credentials)
    {
        var token = await _authenticationService.SignInAsync(credentials);
        return Ok(token);
    }
}