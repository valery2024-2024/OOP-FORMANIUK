using lab25.Core;
using lab25.Factories;
using lab25.Observers;
using lab25.Strategies;
using lab25.Loggers;

namespace lab25;

class Program
{
    static void PrintDivider(string title)
    {
        Console.WriteLine("\n==================================================");
        Console.WriteLine($" {title}");
        Console.WriteLine("==================================================");
    }

    static void Main()
    {
        PrintDivider("Сценарій 1: Повна інтеграція");

        LoggerManager.Initialize(new ConsoleLoggerFactory());

        var context = new DataContext(new EncryptDataStrategy());
        var publisher = new DataPublisher();
        var observer = new ProcessingLoggerObserver();

        publisher.DataProcessed += observer.Update;

        var result = context.Execute("TestData");
        publisher.Publish(result);


        PrintDivider("Сценарій 2: Динамічна зміна логера");

        LoggerManager.Instance.SetFactory(new FileLoggerFactory());

        result = context.Execute("SecondData");
        publisher.Publish(result);

        Console.WriteLine("Дані залоговано у файл log.txt");


        PrintDivider("Сценарій 3: Динамічна зміна стратегії");

        context.SetStrategy(new CompressDataStrategy());

        result = context.Execute("ThirdData");
        publisher.Publish(result);

        Console.WriteLine("Обробка виконана новою стратегією");
        Console.WriteLine("Дані записані у файл log.txt");

        PrintDivider("ДЕМОНСТРАЦІЮ ЗАВЕРШЕНО");
    }
}
