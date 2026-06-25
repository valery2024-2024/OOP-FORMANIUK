var cache =
    new LoggingCacheProxy<int, string>(
        new Cache<int, string>());

cache.Set(1, "Hello RCIT College");

Console.WriteLine(
    cache.Get(1));