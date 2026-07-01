using System.Net;
using System.Text.Json;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Web.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(
        ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(
        HttpContext context,
        RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex, ex.Message);

            await WriteProblemDetails(
                context,
                HttpStatusCode.NotFound,
                "Resource not found",
                "The requested resource could not be found.",
                ex.Message);
        }
        catch (InvalidCredentialsException ex)
        {
            _logger.LogWarning(ex, ex.Message);

            await WriteProblemDetails(
                context,
                HttpStatusCode.Unauthorized,
                "Unauthorized",
                "Authentication failed.",
                ex.Message);
        }
        catch (DomainException ex)
        {
            _logger.LogWarning(ex, ex.Message);

            await WriteProblemDetails(
                context,
                HttpStatusCode.BadRequest,
                "Business rule violation",
                "A business rule has been violated.",
                ex.Message);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, ex.Message);

            await WriteProblemDetails(
                context,
                HttpStatusCode.BadRequest,
                "Invalid request",
                "The request contains invalid data.",
                ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            await WriteProblemDetails(
                context,
                HttpStatusCode.InternalServerError,
                "Internal server error",
                "An unexpected error occurred.",
                "Please contact the system administrator.");
        }
    }

    private static async Task WriteProblemDetails(
        HttpContext context,
        HttpStatusCode statusCode,
        string title,
        string type,
        string detail)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var problem = new ProblemDetails
        {
            Status = (int)statusCode,
            Title = title,
            Type = type,
            Detail = detail
        };

        var json = JsonSerializer.Serialize(problem);

        await context.Response.WriteAsync(json);
    }
}