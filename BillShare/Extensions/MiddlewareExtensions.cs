using BillShare.Middlewares;

namespace BillShare.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this WebApplication app)
    {
        return app.UseMiddleware<ExceptionHandleMiddleware>();
    }
}