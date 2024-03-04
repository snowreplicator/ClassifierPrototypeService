using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Prototype.ClassifierPrototypeService.Bll.Common;

public abstract class Entity
{
    private readonly List<IDomainEvent> _events;

    public object Id { get; }

    public IImmutableList<IDomainEvent> DomainEvents => _events.ToImmutableList();

    protected Entity(object id)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        _events = new List<IDomainEvent>();
    }

    protected void RegisterEvent(IDomainEvent domainEvent)
    {
        _events.Add(domainEvent);
    }

    protected static bool EqualsDictionaryByType<T>(object field, object value)
    {
        if (field is null && value is null) return true;
        if (field is null && value is not null
            || field is not null && value is null) return false;

        Dictionary<string, T> f1 = field as Dictionary<string, T>;
        Dictionary<string, T> f2 = value as Dictionary<string, T>;
        if (f1.Count != f2.Count) return false;

        foreach (KeyValuePair<string, T> kp in f1)
        {
            if (!f2.ContainsKey(kp.Key) || !f1[kp.Key].ToString().Equals(f2[kp.Key].ToString()))
            {
                return false;
            }
        }
        return true;
    }

    protected static bool EqualsDictionary<K>(K field, K value)
    {
        if (field is Dictionary<string, string>)
        {
            return EqualsDictionaryByType<string>(field, value);
        }

        if (field is Dictionary<string, object>)
        {
            return EqualsDictionaryByType<object>(field, value);
        }

        return false;
    }

    protected void RegisterUpdateEvent<K>(ref K field, K value)
    {
        if (EqualsDictionary(field, value)
            || EqualityComparer<K>.Default.Equals(field, value))
            return;

        RegisterUpdateEvent();
    }

    public abstract void RegisterUpdateEvent();

    protected void ClearEvents()
    {
        _events.Clear();
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return obj != null
               && obj is Entity otherEntity
               && Id == otherEntity.Id;
    }
}

public abstract class Entity<TIdentity> : Entity
{
    public new TIdentity Id { get; }

    protected Entity(TIdentity id) : base(id)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
    }
}
