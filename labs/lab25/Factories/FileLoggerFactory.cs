using lab25.Loggers;

namespace lab25.Factories;

public class FileLoggerFactory : ILoggerFactory
{
    public ILogger CreateLogger()
    {
        return new FileLogger();
    }
}
