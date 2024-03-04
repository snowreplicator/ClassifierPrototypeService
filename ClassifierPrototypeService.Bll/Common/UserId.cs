namespace Prototype.ClassifierPrototypeService.Bll.Common;

public sealed record UserId
{
    private readonly string _value;

    public bool IsEmpty => string.IsNullOrEmpty(_value);

    public static UserId Empty { get; } = new UserId(string.Empty);

    private UserId(string id)
    {
        _value = id;
    }

    public static UserId FromString(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Empty;
        return new UserId(value);
    }

    public override string ToString() => _value;
}
