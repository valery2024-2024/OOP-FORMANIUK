namespace lab21v1.Strategies
{
    // Standard - середнє
    public class StandardTaxiStrategy : ITaxiStrategy
    {
        public decimal CalculateCost(decimal distanceKm, decimal waitingMinutes)
        {
            // логіка - 15 грн/км + 2 грн/хв + 20 грн фіксовано
            return distanceKm * 15m + waitingMinutes * 2m + 20m;
        }
    }
}
