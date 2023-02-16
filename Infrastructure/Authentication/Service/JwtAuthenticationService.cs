using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Contracts.Authentication;
using Contracts.Responses;
using Domain.Models;
using Infrastructure.Authentication.Constants;
using Infrastructure.Authentication.Extensions;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using Services.Abstractions.Authentication;

namespace Infrastructure.Authentication.Service;
public class JwtAuthenticationService : IJwtAuthenticationService
{
    private readonly ICustomerService _customerService;
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly AuthenticationOptions _options;

    public JwtAuthenticationService(ICustomerService customerService, AuthenticationOptions options)
    {
        _customerService = customerService;
        _options = options;
        _tokenHandler = new JwtSecurityTokenHandler();
    }

    private string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var deadline = DateTime.UtcNow.Add(_options.AccessTokenLifetime);
        var signinCredentials = new SigningCredentials(_options.GenerateSecurityKey(), SecurityAlgorithms.HmacSha256);
        var jwt = new JwtSecurityToken(_options.Issuer, _options.Audience, claims, expires: deadline,
            signingCredentials: signinCredentials);
        var accessToken = _tokenHandler.WriteToken(jwt)!;
        return accessToken;
    }

    private RefreshToken GenerateRefreshToken(CustomerResponse customer)
    {
        using var rng = RandomNumberGenerator.Create();
        var data = new byte[AuthenticationConstants.RefreshTokenLength];
        rng.GetNonZeroBytes(data);
        var token = Convert.ToBase64String(data);
        var expirationDate = DateTime.UtcNow.Add(_options.RefreshTokenLifetime);
        var refreshToken = new RefreshToken
        {
            OwnerId = customer.Id,
            ExpirationDateTime = expirationDate,
            Token = token
        };
        return refreshToken;
    }

    private AuthenticationToken GenerateJwtToken(CustomerResponse customer)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, customer.Name),
            new Claim(CustomClaimTypes.Uid, customer.Id.ToString())
        };
        var accessToken = GenerateAccessToken(claims);
        var refreshToken = GenerateRefreshToken(customer);
        var token = new AuthenticationToken
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token
        };
        return token;
    }

    public async Task<AuthenticationToken> SignInAsync(SignInUserCredentials credentials, CancellationToken token)
    {
        var customer = await _customerService.GetCustomerByCredentialsAsync(credentials, token);
        return GenerateJwtToken(customer);
    }

    public async Task<AuthenticationToken> SignUpAsync(SignUpUserCredentials credentials, CancellationToken token = default)
    {
        var customer = await _customerService.CreateCustomerAsync(credentials, token);
        return GenerateJwtToken(customer);
    }
}