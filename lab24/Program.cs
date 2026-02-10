using System;

class Program
{
    static void Main()
    {
        var processor = new NumericProcessor(new SquareOperationStrategy());
        var publisher = new ResultPublisher();

        var consoleObserver = new ConsoleLoggerObserver();
        var historyObserver = new HistoryLoggerObserver();
        var thresholdObserver = new ThresholdNotifierObserver(50);

        publisher.ResultCalculated += consoleObserver.OnResultCalculated;
        publisher.ResultCalculated += historyObserver.OnResultCalculated;
        publisher.ResultCalculated += thresholdObserver.OnResultCalculated;

        double[] values = { 4, 5, 10 };

        processor.SetStrategy(new SquareOperationStrategy());
        foreach (var v in values)
        {
            var result = processor.Process(v);
            publisher.PublishResult(result, "Квадрат");
        }

        processor.SetStrategy(new CubeOperationStrategy());
        foreach (var v in values)
        {
            var result = processor.Process(v);
            publisher.PublishResult(result, "Куб");
        }

        processor.SetStrategy(new SquareRootOperationStrategy());
        foreach (var v in values)
        {
            var result = processor.Process(v);
            publisher.PublishResult(result, "Квадратний корінь");
        }

        Console.WriteLine("\nІсторія результатів:");
        foreach (var record in historyObserver.History)
        {
            Console.WriteLine(record);
        }
    }
}