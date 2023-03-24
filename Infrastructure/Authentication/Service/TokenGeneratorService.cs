using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using AutoMapper;
using Contracts.Authentication;
using Contracts.Responses;
using Contracts.Responses.Customers;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Authentication.Constants;
using Infrastructure.Authentication.Extensions;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions.Authentication;

namespace Infrastructure.Authentication.Service;

public class TokenGeneratorService : ITokenGeneratorService
{
    private readonly AuthenticationOptions _options;
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TokenGeneratorService(AuthenticationOptions options, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _options = options;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _tokenHandler = new JwtSecurityTokenHandler();
    }

    private string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var deadline = DateTime.UtcNow.Add(_options.AccessTokenLifetime);
        var key = _options.GenerateSecurityKey();
        const string securityAlgorithm = SecurityAlgorithms.HmacSha256;
        var signinCredentials = new SigningCredentials(key, securityAlgorithm);
        var jwt = new JwtSecurityToken(_options.Issuer, _options.Audience, claims, expires: deadline,
            signingCredentials: signinCredentials);
        var accessToken = _tokenHandler.WriteToken(jwt)!;
        return accessToken;
    }

    private async Task<RefreshToken> GenerateRefreshTokenAsync(CustomerResponse customer,
        CancellationToken cancellationToken = default)
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
        await _unitOfWork.RefreshTokenRepository.AddRefreshTokenAsync(refreshToken, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return refreshToken;
    }

    public async Task<AuthenticationToken> GenerateJwtTokenAsync(CustomerResponse customer,
        CancellationToken cancellationToken = default)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, customer.Name),
            new Claim(CustomClaimTypes.Uid, customer.Id.ToString())
        };
        var accessToken = GenerateAccessToken(claims);
        var refreshToken = await GenerateRefreshTokenAsync(customer, cancellationToken);
        var token = new AuthenticationToken
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token
        };
        return token;
    }

    public async Task<AuthenticationToken> RefreshJwtTokenAsync(string refreshToken,
        CancellationToken cancellationToken = default)
    {
        var refreshTokenOwner = await _unitOfWork.RefreshTokenRepository
            .GetRefreshTokenOwnerAsync(refreshToken, cancellationToken);
        var customer = _mapper.Map<CustomerResponse>(refreshTokenOwner);
        return await GenerateJwtTokenAsync(customer, cancellationToken);
    }
}