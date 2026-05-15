using System.Diagnostics;
using System.Text.Json;
using Dapper;
using Npgsql;

namespace WebApi.Middlewares;

public class LogginMiddleware
{
    public readonly RequestDelegate _next;

    public LogginMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        System.Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");

        System.Console.WriteLine($"Request body: {context.Request.ContentType}");

        await _next(context);

        stopwatch.Stop();

        System.Console.WriteLine(
            $"Response: {context.Response.StatusCode} processed in {stopwatch.ElapsedMilliseconds} ms"
        );
    }
}