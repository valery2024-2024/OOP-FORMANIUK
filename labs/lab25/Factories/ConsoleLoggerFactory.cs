using lab25.Loggers;

namespace lab25.Factories;

public class ConsoleLoggerFactory : ILoggerFactory
{
    public ILogger CreateLogger()
    {
        return new ConsoleLogger();
    }
}
