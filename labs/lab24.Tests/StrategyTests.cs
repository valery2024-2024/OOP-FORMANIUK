using Xunit;

public class StrategyTests
{
    [Fact]
    public void SquareStrategy_ReturnsCorrectResult()
    {
        var strategy = new SquareOperationStrategy();
        var processor = new NumericProcessor(strategy);

        double result = processor.Process(4);

        Assert.Equal(16, result);
    }

    [Fact]
    public void CubeStrategy_ReturnsCorrectResult()
    {
        var strategy = new CubeOperationStrategy();
        var processor = new NumericProcessor(strategy);

        double result = processor.Process(3);

        Assert.Equal(27, result);
    }

    [Fact]
    public void SquareRootStrategy_ReturnsCorrectResult()
    {
        var strategy = new SquareRootOperationStrategy();
        var processor = new NumericProcessor(strategy);

        double result = processor.Process(9);

        Assert.Equal(3, result);
    }

    [Fact]
    public void CanChangeStrategyAtRuntime()
    {
        var processor = new NumericProcessor(new SquareOperationStrategy());

        double squareResult = processor.Process(5);
        processor.SetStrategy(new CubeOperationStrategy());
        double cubeResult = processor.Process(5);

        Assert.Equal(25, squareResult);
        Assert.Equal(125, cubeResult);
    }
}
