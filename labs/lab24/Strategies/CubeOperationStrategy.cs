public class CubeOperationStrategy : INumericOperationStrategy
{
    public double Execute(double value)
    {
        return value * value * value;
    }
}