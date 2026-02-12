using lab25.Loggers;

namespace lab25.Factories;

public interface ILoggerFactory
{
    ILogger CreateLogger();
}
