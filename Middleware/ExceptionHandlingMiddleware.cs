namespace MovieLog.Middleware;
// Mideleware care prinde orice exceptie din pipeline si raspunde cu JSON si status code corect
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
   
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try {
            await _next(context);
        }
        catch (Exception ex)
        {
            // daca ceva arunca exceptie, logam cu stack trace complet
            _logger.LogError(ex, "An unhandled exception on {Method} {Path}.",
                context.Request.Method, context.Request.Path);
            // construim rasp JSON in loc sa crape aplicatia
            await HandleExceptionAsync(context, ex);
        }

    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, message) = exception switch
        {
            UnauthorizedAccessException => (StatusCodes.Status403Forbidden, "Unauthorized access."),
            KeyNotFoundException => (StatusCodes.Status404NotFound, "Resource not found."),
            ArgumentException => (StatusCodes.Status400BadRequest, "Invalid request."),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
        };

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        // rasp JSON standardizat
        return context.Response.WriteAsJsonAsync(new 
        { 
            success = false,
            message
        });
    }
}
