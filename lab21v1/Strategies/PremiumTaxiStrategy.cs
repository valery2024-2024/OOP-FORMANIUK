namespace lab21v1.Strategies
{
    // Premium - дорожче (тому що комфорт)
    public class PremiumTaxiStrategy : ITaxiStrategy
    {
        public decimal CalculateCost(decimal distanceKm, decimal waitingMinutes)
        {
            // логіка - 20 грн/км + 3 грн/хв + 50 грн фіксовано
            return distanceKm * 20m + waitingMinutes * 3m + 50m;
        }
    }
}
