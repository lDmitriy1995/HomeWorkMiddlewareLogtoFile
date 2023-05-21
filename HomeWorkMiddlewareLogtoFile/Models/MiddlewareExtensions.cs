using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionLoggingMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionLoggingMiddleware>();
    }
}

public class Startup
{
    // ...

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        // ...

        app.UseExceptionLoggingMiddleware();

        // ...
    }
}
