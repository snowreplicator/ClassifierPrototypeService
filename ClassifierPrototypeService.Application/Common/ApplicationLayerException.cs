using System;

namespace Prototype.ClassifierPrototypeService.Application.Common;

public sealed class ApplicationLayerException : Exception
{
    public ApplicationLayerException(string message, string code, Exception exception = null) : base(message, exception)
    {
        Code = code;
    }
    public string Code { get; set; }
}
