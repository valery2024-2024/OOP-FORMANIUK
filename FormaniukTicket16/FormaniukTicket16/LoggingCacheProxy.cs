public class LoggingCacheProxy<TKey, TValue>
    : ICache<TKey, TValue>
{
    private readonly ICache<TKey, TValue>
        _cache;

    public LoggingCacheProxy(
        ICache<TKey, TValue> cache)
    {
        _cache = cache;
    }

    public TValue Get(TKey key)
    {
        Console.WriteLine(
            $"GET: {key}");

        return _cache.Get(key);
    }

    public void Set(
        TKey key,
        TValue value)
    {
        Console.WriteLine(
            $"SET: {key}");

        _cache.Set(key, value);
    }
}