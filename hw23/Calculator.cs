public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Divide(int a, int b)
    {
        if (b == 0)
            throw new DivideByZeroException("На нуль ділити не можна");

        return a / b;
    }

    public bool IsEven(int number)
    {
        return number % 2 == 0;
    }
}