namespace lab21v1.Strategies
{
    public interface ITaxiStrategy
    {
        decimal CalculateCost(decimal distanceKm, decimal waitingMinutes);
    }
}
