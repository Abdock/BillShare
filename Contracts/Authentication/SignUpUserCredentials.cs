﻿namespace Contracts.Authentication;

public class SignUpUserCredentials
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string Username { get; init; }
}