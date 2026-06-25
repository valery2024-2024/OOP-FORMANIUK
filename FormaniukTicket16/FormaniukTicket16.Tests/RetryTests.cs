using Xunit;

public class RetryTests
{
    [Fact]
    public void ExecuteWithRetry_ShouldRetry()
    {
        // Arrange
        int attempts = 0;

        // Act
        RetryHelper.ExecuteWithRetry(() =>
        {
            attempts++;

            if (attempts < 3)
                throw new Exception();
        });

        // Assert
        Assert.Equal(3, attempts);
    }
}