using System;
using Microsoft.Extensions.Logging;

namespace Prototype.ClassifierPrototypeService.Application.Helpers;

public abstract class LoggingHandler
{
    private readonly ILogger _logger;

    public LoggingHandler(ILogger logger)
    {
        _logger = logger;
    }

    protected IDisposable BeginLoggerScope(string tracingId)
    {
        return _logger.BeginScope("{Scope}", tracingId);
    }

    protected void LogInfo(string message)
    {
        Log(LogLevel.Information, message);
    }

    protected void LogTrace(string message)
    {
        Log(LogLevel.Trace, message);
    }

    protected void LogWarn(string message, Exception exception = null)
    {
        Log(LogLevel.Warning, message, exception);
    }

    protected void LogError(string message, Exception exception = null)
    {
        Log(LogLevel.Error, message, exception);
    }

    protected void LogCrit(string message, Exception exception = null)
    {
        Log(LogLevel.Critical, message, exception);
    }

    private void Log(LogLevel logLevel, string message, Exception exception = null)
    {
        if (exception is null)
        {
            _logger.Log(logLevel, message);
        }
        else
        {
            _logger.Log(logLevel, exception, message);
        }
    }
}
