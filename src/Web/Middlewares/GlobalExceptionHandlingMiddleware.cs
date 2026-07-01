using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Net;
using Domain.Exceptions;
namespace Web.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
    public GlobalExceptionHandlingMiddleware(
        ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, ex.Message);
            int statusCode = (int)HttpStatusCode.NotFound;
            context.Response.StatusCode = statusCode;
            ProblemDetails problem = new()
            {
                Status = statusCode,
                Type = "NotFound",
                Title = "Not Found",
                Detail = ex.Message
            };
            string json = JsonSerializer.Serialize(problem);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
        catch (InvalidCredentialsException ex)
        {
            _logger.LogError(ex, ex.Message);

            int statusCode = (int)HttpStatusCode.Unauthorized;

            context.Response.StatusCode = statusCode;

            ProblemDetails problem = new()
            {
                Status = statusCode,
                Type = "Authentication error",
                Title = "Authentication error",
                Detail = ex.Message
            };

            string json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
        catch (DuplicateApplicationException ex)
        {
            _logger.LogError(ex, ex.Message);

            int statusCode = (int)HttpStatusCode.Conflict;

            context.Response.StatusCode = statusCode;

            ProblemDetails problem = new()
            {
                Status = statusCode,
                Type = "Conflict",
                Title = "Conflict",
                Detail = ex.Message
            };

            string json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
        catch (InvalidStatusException ex)
        {
            _logger.LogError(ex, ex.Message);

            int statusCode = (int)HttpStatusCode.BadRequest;

            context.Response.StatusCode = statusCode;

            ProblemDetails problem = new()
            {
                Status = statusCode,
                Type = "Invalid status",
                Title = "Invalid status",
                Detail = ex.Message
            };

            string json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
        catch (ForbiddenException ex)
        {
            _logger.LogError(ex, ex.Message);

            int statusCode = (int)HttpStatusCode.Forbidden;

            context.Response.StatusCode = statusCode;

            ProblemDetails problem = new()
            {
                Status = statusCode,
                Type = "Forbidden",
                Title = "Forbidden",
                Detail = ex.Message
            };

            string json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex, ex.Message);

            int statusCode = (int)HttpStatusCode.BadRequest;

            context.Response.StatusCode = statusCode;

            ProblemDetails problem = new()
            {
                Status = statusCode,
                Type = "Bad request",
                Title = "Bad request",
                Detail = ex.Message
            };

            string json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            _logger.LogError(ex, ex.Message);

            int statusCode = (int)HttpStatusCode.BadRequest;

            context.Response.StatusCode = statusCode;

            ProblemDetails problem = new()
            {
                Status = statusCode,
                Type = "Bad request",
                Title = "Bad request",
                Detail = ex.Message
            };

            string json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
        /*  catch (AppValidationException ex)
          {
              _logger.LogError(ex, ex.Message);
              int statusCode = (int)HttpStatusCode.BadRequest;
              context.Response.StatusCode = statusCode;
              ProblemDetails problem = new()
              {
                  Status = statusCode,
                  Type = "Server error",
                  Title = "Server error",
                  Detail = ex.Message
              };
              string json = JsonSerializer.Serialize(problem);
              context.Response.ContentType = "application/json";
              await context.Response.WriteAsync(json);
          } */
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, ex.Message);
            int statusCode = (int)HttpStatusCode.BadRequest;
            context.Response.StatusCode = statusCode;
            ProblemDetails problem = new()
            {
                Status = statusCode,
                Type = "Bad request",
                Title = "Bad request",
                Detail = ex.Message
            };
            string json = JsonSerializer.Serialize(problem);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
        catch (Exception ex)
        {
            var innerMessage = ex.InnerException?.Message ?? "Sin inner exception";
            var innerInnerMessage = ex.InnerException?.InnerException?.Message ?? "";

            _logger.LogError(ex, ex.Message);
            int statusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.StatusCode = statusCode;
            ProblemDetails problem = new()
            {
                Status = statusCode,
                Type = "Server error",
                Title = "Server error",
                Detail = ex.Message
            };
            string json = JsonSerializer.Serialize(problem);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
    }
}
