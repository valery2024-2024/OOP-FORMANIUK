using System.Collections.Generic;

public class HistoryLoggerObserver
{
    public List<string> History { get; } = new();

    public void OnResultCalculated(double result, string operationName)
    {
        History.Add($"{operationName}: {result}");
    }
}