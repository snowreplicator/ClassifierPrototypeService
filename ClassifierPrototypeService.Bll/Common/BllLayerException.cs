using System;

namespace Prototype.ClassifierPrototypeService.Bll.Common;

public sealed class BllLayerException : Exception
{
    public BllLayerException(string message, string code, Exception exception = null) : base(message, exception)
    {
        Code = code;
    }
    public string Code { get; set; }
}
