using BillShare.Responses;
using Domain.Exceptions;

namespace BillShare.Middlewares;

public class ExceptionHandleMiddleware
{
    private readonly ILogger<ExceptionHandleMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandleMiddleware(ILogger<ExceptionHandleMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError("Error, message: {ExceptionMessage}; stack trace: {ExceptionStackTrace}",
                exception.Message, exception.StackTrace);
            await HandleExceptionAsync(context, exception);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var error = exception switch
        {
            NotFoundException => new ErrorResponse
            {
                StatusCode = StatusCodes.Status404NotFound, Message = exception.Message
            },
            NotEnoughPermissionsException => new ErrorResponse
            {
                StatusCode = StatusCodes.Status403Forbidden, Message = exception.Message
            },
            UnknownUserException => new ErrorResponse
            {
                StatusCode = StatusCodes.Status401Unauthorized, Message = exception.Message
            },
            UserAlreadyExistsException => new ErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest, Message = exception.Message
            },
            _ => new ErrorResponse {StatusCode = StatusCodes.Status500InternalServerError, Message = "Something wrong"}
        };

        context.Response.StatusCode = error.StatusCode;
        await context.Response.WriteAsJsonAsync(error);
    }
}