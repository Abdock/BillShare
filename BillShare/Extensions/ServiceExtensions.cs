﻿using System.Reflection;
using System.Text.Json.Serialization;
using BillShare.Constants;
using Contracts.Authentication;
using Domain.Repositories;
using Infrastructure.Authentication.Extensions;
using Infrastructure.Authentication.Service;
using Infrastructure.Database.Context;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services;
using Services.Abstractions;
using Services.Abstractions.Authentication;

namespace BillShare.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(ConfigurationConstants.JwtConfig);
        var options = section.Get<AuthenticationOptions>()!;
        services.AddScoped<AuthenticationOptions>(_ => options);
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(jwtOptions =>
            {
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = options.Issuer,
                    ValidAudience = options.Audience,
                    IssuerSigningKey = options.GenerateSecurityKey(),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                };
            });
        services.AddAuthorization();
        return services;
    }

    public static IServiceCollection ConfigureControllers(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                var enumConverter = new JsonStringEnumConverter();
                options.JsonSerializerOptions.Converters.Add(enumConverter);
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
        return services;
    }

    public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(ConfigurationConstants.DefaultConnectionString));
        });
    }

    public static IServiceCollection ConfigureCors(this IServiceCollection services)
    {
        return services.AddCors(options =>
        {
            options.AddPolicy(CorsProfiles.AllowsAll, builder =>
            {
                builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            });
        });
    }

    public static IServiceCollection ConfigureCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
        services.AddScoped<ICustomerService, CustomerService>();
        return services;
    }

    public static IServiceCollection ConfigureMapper(this IServiceCollection services)
    {
        return services.AddAutoMapper(expression =>
        {
            var contractsLayer = Assembly.Load(nameof(Contracts));
            expression.AddMaps(contractsLayer);
        });
    }
}