using System.Text.Json.Serialization;
using BillShare.Constants;
using Domain.Repositories;
using Infrastructure.Auth.Interfaces;
using Infrastructure.Auth.Models;
using Infrastructure.Auth.Service;
using Infrastructure.Database.Context;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BillShare.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IJwtAuthenticationService, JwtAuthenticationService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
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
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
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
}