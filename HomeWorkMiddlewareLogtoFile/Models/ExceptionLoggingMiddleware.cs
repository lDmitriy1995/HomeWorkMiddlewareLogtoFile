using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public class ExceptionLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionLoggingMiddleware> _logger;

    public ExceptionLoggingMiddleware(RequestDelegate next, ILogger<ExceptionLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            LogExceptionToFile(ex);
            throw;
        }
    }

    private void LogExceptionToFile(Exception ex)
    {
        string logFilePath = "error.log";
        string errorMessage = $"[{DateTime.Now}] An exception occurred: {ex.Message}";

        try
        {
            using (StreamWriter writer = new StreamWriter(logFilePath, append: true))
            {
                writer.WriteLine(errorMessage);
            }
        }
        catch (Exception)
        {
            // Если не удалось записать ошибку в файл, то игнорируем и продолжаем выполнение
            _logger.LogError("Failed to log exception to file.");
        }
    }
}
