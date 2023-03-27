using System.Reflection;
using System.Text.Json.Serialization;
using BillShare.Constants;
using Contracts.Authentication;
using Domain.Repositories;
using Infrastructure.Authentication.Extensions;
using Infrastructure.Authentication.Service;
using Infrastructure.Database.Context;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services.Abstractions;
using Services.Abstractions.Authentication;

namespace BillShare.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureAuthenticationAndAuthorization(this IServiceCollection services,
        IConfiguration configuration)
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
            options.UseNpgsql(configuration.GetConnectionString(ConfigurationConstants.DefaultConnectionString));
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
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IServiceManager, ServiceManager>();
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
    
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        const string authScheme = JwtBearerDefaults.AuthenticationScheme;
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc($"v {1}", new OpenApiInfo
            {
                Title = "Bill share API",
                Version = $"v {1}",
                Description = "Bill share API Services."
            });
            options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            options.AddSecurityDefinition(authScheme, new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = authScheme,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = authScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}