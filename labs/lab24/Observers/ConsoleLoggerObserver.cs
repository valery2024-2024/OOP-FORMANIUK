using System;

public class ConsoleLoggerObserver
{
    public void OnResultCalculated(double result, string operationName)
    {
        Console.WriteLine($"Операція: {operationName}, результат: {result}");
    }
}