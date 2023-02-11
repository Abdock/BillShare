using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Domain.Enums;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Auth.Constants;
using Infrastructure.Auth.Interfaces;
using Infrastructure.Auth.Models;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Auth.Service;

public class JwtAuthenticationService : IJwtAuthenticationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly AuthenticationOptions _options;

    public JwtAuthenticationService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, AuthenticationOptions options)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
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

    private RefreshToken GenerateRefreshToken(Customer customer)
    {
        using var rng = RandomNumberGenerator.Create();
        var data = new byte[AuthenticationConstants.RefreshTokenLength];
        rng.GetNonZeroBytes(data);
        var token = Convert.ToBase64String(data);
        var expirationDate = DateTime.UtcNow.Add(_options.RefreshTokenLifetime);
        var refreshToken = new RefreshToken
        {
            Owner = customer,
            OwnerId = customer.Id,
            ExpirationDateTime = expirationDate,
            Token = token
        };
        return refreshToken;
    }

    private AuthenticationToken GenerateJwtToken(Customer customer)
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
        var customer = await _unitOfWork.CustomerRepository
            .GetByCredentialsAsync(credentials.Username, credentials.Password, token);
        return GenerateJwtToken(customer);
    }

    public async Task<AuthenticationToken> SignUpAsync(SignUpUserCredentials credentials, CancellationToken token = default)
    {
        var customer = new Customer
        {
            Email = credentials.Email,
            Name = credentials.Username,
            RoleId = RoleId.User
        };
        var passwordHash = _passwordHasher.HashPassword(customer, credentials.Password);
        customer.Password = passwordHash;
        await _unitOfWork.CustomerRepository.AddCustomerAsync(customer, token);
        await _unitOfWork.SaveChangesAsync(token);
        return GenerateJwtToken(customer);
    }
}