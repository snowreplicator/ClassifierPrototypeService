using System;

namespace Prototype.ClassifierPrototypeService.Application.Common;

public record TracingId
{
    public Guid Value { get; }

    public TracingId()
    {
        Value = Guid.NewGuid();
    }

    private TracingId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentNullException(nameof(value), "TracingId cannot be empty");

        Value = value;
    }

    public override string ToString() 
        => Value.ToString("N");

    public static TracingId FromGuid(Guid value) 
        => new(value);
}
