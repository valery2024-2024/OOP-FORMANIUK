using lab25.Factories;

namespace lab25.Loggers;

public class LoggerManager
{
    private static LoggerManager? _instance;
    private ILogger _logger;

    private LoggerManager(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger();
    }

    public static LoggerManager Instance
        => _instance ?? throw new Exception("LoggerManager not initialized!");

    public static void Initialize(ILoggerFactory factory)
    {
        _instance = new LoggerManager(factory);
    }

    public void SetFactory(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger();
    }

    public void Log(string message)
    {
        _logger.Log(message);
    }
}
