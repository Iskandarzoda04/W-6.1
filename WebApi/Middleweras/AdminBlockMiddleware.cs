using System.Diagnostics;
using System.Text.Json;
using Dapper;
using Npgsql;
namespace WebApi.Middlewares;

public class AdminBlockMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AdminBlockMiddleware> _logger;

    public AdminBlockMiddleware(RequestDelegate next, ILogger<AdminBlockMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            if (context.Request.Path.StartsWithSegments("/admin"))
            {
                _logger.LogWarning("Admin access blocked");

                context.Response.StatusCode = 403;

                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    Error = "Access denied"
                }));

                return;
            }

            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            context.Response.StatusCode = 500;

            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                Error = "Internal server error"
            }));
        }
    }
}