using System.Collections.Generic;

public class Cache<TKey, TValue> : ICache<TKey, TValue>
    where TKey : notnull
{
    private readonly Dictionary<TKey, TValue> _storage = new();

    public TValue Get(TKey key)
    {
        return _storage[key];
    }

    public void Set(TKey key, TValue value)
    {
        _storage[key] = value;
    }
}