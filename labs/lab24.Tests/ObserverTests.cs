using Xunit;

public class ObserverTests
{
    [Fact]
    public void ConsoleObserver_IsNotNull()
    {
        var observer = new ConsoleLoggerObserver();
        Assert.NotNull(observer);
    }

    [Fact]
    public void HistoryObserver_SavesResults()
    {
        var publisher = new ResultPublisher();
        var historyObserver = new HistoryLoggerObserver();

        publisher.ResultCalculated += historyObserver.OnResultCalculated;

        publisher.PublishResult(10, "Test");
        publisher.PublishResult(20, "Test2");

        Assert.Equal(2, historyObserver.History.Count);
        Assert.Contains("Test: 10", historyObserver.History);
        Assert.Contains("Test2: 20", historyObserver.History);
    }

    [Fact]
    public void ThresholdObserver_DoesNotThrowException()
    {
        var publisher = new ResultPublisher();
        var observer = new ThresholdNotifierObserver(50);

        publisher.ResultCalculated += observer.OnResultCalculated;

        var exception = Record.Exception(() =>
        {
            publisher.PublishResult(100, "Test");
        });

        Assert.Null(exception);
    }
}
