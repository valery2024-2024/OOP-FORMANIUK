using System;
using Xunit;

public class CalculatorTests
{
    [Fact]
    public void Add_PositiveNumbers_ReturnsSum()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        int result = calculator.Add(5, 3);

        // Assert
        Assert.Equal(8, result);
    }

    [Fact]
    public void Add_WithZero_ReturnsSameNumber()
    {
        var calculator = new Calculator();

        int result = calculator.Add(10, 0);

        Assert.Equal(10, result);
    }

    [Fact]
    public void Divide_ValidNumbers_ReturnsResult()
    {
        var calculator = new Calculator();

        int result = calculator.Divide(10, 2);

        Assert.Equal(5, result);
    }

    [Fact]
    public void Divide_ByZero_ThrowsException()
    {
        var calculator = new Calculator();

        Assert.Throws<DivideByZeroException>(() => calculator.Divide(10, 0));
    }

    [Fact]
    public void IsEven_EvenNumber_ReturnsTrue()
    {
        var calculator = new Calculator();

        bool result = calculator.IsEven(4);

        Assert.True(result);
    }

    [Fact]
    public void IsEven_OddNumber_ReturnsFalse()
    {
        var calculator = new Calculator();

        bool result = calculator.IsEven(5);

        Assert.False(result);
    }
}