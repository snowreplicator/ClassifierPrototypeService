using System;
using Newtonsoft.Json;

namespace Prototype.ClassifierPrototypeService.Middleware;

public class ServiceException : Exception
{
    private string ErrorCode { get; set; } = "Unknown";
    private new Exception InnerException { get; set; }

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
        ErrorCode = errorCode;
        InnerException = innerException;
    }

    public override string ToString() 
        => JsonConvert.SerializeObject(
            new
            {
                IsError = true,
                Code = ErrorCode, 
                Message = Message,
                ExceptionType = InnerException.GetType(),
                Source = InnerException.Source,
                InnerException = InnerException,
                StackTrace = StackTrace
            });
}
