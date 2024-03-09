using System;
using Microsoft.Extensions.Logging;
using Prototype.ClassifierPrototypeService.Application.Common;

namespace Prototype.ClassifierPrototypeService.Application.Helpers;

public abstract class WrappingToBaseException : LoggingHandler
{
    protected WrappingToBaseException(ILogger logger) : base(logger)
    {
    }
    
    protected abstract ApplicationLayerException WrapException(Exception exception);
}
