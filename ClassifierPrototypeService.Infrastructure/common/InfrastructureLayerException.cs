using System;

namespace Prototype.ClassifierPrototypeService.Infrastructure.common;

public sealed class InfrastructureLayerException : Exception
{
    public string Code { get; set; }
    
    public InfrastructureLayerException(string message, string code, Exception exception = null) : base(message, exception)
    {
        Code = code;
    }
}
