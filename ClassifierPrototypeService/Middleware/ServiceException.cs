using System;

namespace Prototype.ClassifierPrototypeService.Middleware;

public class ServiceException : Exception
{
    public string ErrorCode { get; private set; } = "Unknown";

    public ServiceException()
    {
    }

    public ServiceException(string message, string errorCode)
        : base(message)
    {
        this.ErrorCode = errorCode;
    }

    public ServiceException(string message, string errorCode, Exception innerException)
        : base(message, innerException)
    {
        this.ErrorCode = errorCode;
    }
}
