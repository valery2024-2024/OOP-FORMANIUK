using lab25.Loggers;

namespace lab25.Observers;

public class ProcessingLoggerObserver : IObserver
{
    public void Update(string message)
    {
        LoggerManager.Instance.Log(message);
    }
}
