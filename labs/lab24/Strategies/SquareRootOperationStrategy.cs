using System;

public class SquareRootOperationStrategy : INumericOperationStrategy
{
    public double Execute(double value)
    {
        return Math.Sqrt(value);
    }
}