namespace lab21v1.Strategies
{
    // Economy - дешевше
    public class EconomyTaxiStrategy : ITaxiStrategy
    {
        public decimal CalculateCost(decimal distanceKm, decimal waitingMinutes)
        {
            // логіка - 12 грн/км + 1.5 грн/хв
            return distanceKm * 12m + waitingMinutes * 1.5m;
        }
    }
}
