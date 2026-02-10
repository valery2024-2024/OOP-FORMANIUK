public class SquareOperationStrategy : INumericOperationStrategy
{
    public double Execute(double value)
    {
        return value * value;
    }
}