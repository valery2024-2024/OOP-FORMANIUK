using System;

public class ThresholdNotifierObserver
{
    private readonly double _threshold;

    public ThresholdNotifierObserver(double threshold)
    {
        _threshold = threshold;
    }

    public void OnResultCalculated(double result, string operationName)
    {
        if (result > _threshold)
        {
            Console.WriteLine($"Увага! Результат {result} перевищує поріг {_threshold}");
        }
    }
}