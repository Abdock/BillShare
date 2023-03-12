﻿using Contracts.Authentication;
using Contracts.Responses;

namespace Services.Abstractions.Authentication;

public interface ITokenGeneratorService
{
    Task<AuthenticationToken> GenerateJwtTokenAsync(CustomerResponse customerResponse,
        CancellationToken cancellationToken = default);

    Task<AuthenticationToken> RefreshJwtTokenAsync(string refreshToken,
        CancellationToken cancellationToken = default);
}