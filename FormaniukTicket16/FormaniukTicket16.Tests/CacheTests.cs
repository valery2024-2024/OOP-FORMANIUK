using Xunit;

public class CacheTests
{
    [Fact]
    public void SetAndGet_ShouldReturnValue()
    {
        // Arrange
        var cache =
            new Cache<int, string>();

        // Act
        cache.Set(1, "Test");

        string result =
            cache.Get(1);

        // Assert
        Assert.Equal(
            "Test",
            result);
    }

    [Fact]
    public void Proxy_ShouldReturnValue()
    {
        // Arrange
        var cache =
            new LoggingCacheProxy<int, string>(
                new Cache<int, string>());

        // Act
        cache.Set(1, "Hello");

        string result =
            cache.Get(1);

        // Assert
        Assert.Equal(
            "Hello",
            result);
    }
}