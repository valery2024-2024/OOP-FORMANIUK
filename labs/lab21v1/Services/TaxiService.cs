using lab21v1.Strategies;

namespace lab21v1.Services
{
    public class TaxiService
    {
        // важливо для OCP сервіс не залежить від конкретних класів
        // тільки від інтерфейсу ITaxiStrategy
        public decimal CalculateRideCost(decimal distanceKm, decimal waitingMinutes, ITaxiStrategy strategy)
        {
            return strategy.CalculateCost(distanceKm, waitingMinutes);
        }
    }
}
