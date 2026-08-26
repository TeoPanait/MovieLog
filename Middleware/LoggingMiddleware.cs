using System.Diagnostics;

namespace MovieLog.Middleware;

// Middleware care logheaza fiecare request: method, path, status code si durata
public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // pornim cronometrul
        var stopwatch = Stopwatch.StartNew();
        var method = context.Request.Method;
        var path = context.Request.Path;

        // log de intrare - inca nu stim status code-ul
        _logger.LogInformation("-> {Method} {Path}", method, path);

        // trimitem request-ul mai departe (routing, controller, EF etc.)
        await _next(context);

        // stim si status code-ul si cat a durat
        stopwatch.Stop();
        var statusCode = context.Response.StatusCode;
        _logger.LogInformation("<- {Method} {Path} -> {StatusCode} ({Duration}ms)",
            method, path, statusCode, stopwatch.ElapsedMilliseconds);
    }
}