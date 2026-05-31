namespace lab21v1.Strategies
{
    // Night - нічна поїздка, надбавка до стандарт
    public class NightTaxiStrategy : ITaxiStrategy
    {
        public decimal CalculateCost(decimal distanceKm, decimal waitingMinutes)
        {
            // береться тандартний тариф як база
            decimal baseCost = distanceKm * 15m + waitingMinutes * 2m + 20m;

            // нічна надбавка (фіксована)
            decimal nightExtra = 30m;

            return baseCost + nightExtra;
        }
    }
}
