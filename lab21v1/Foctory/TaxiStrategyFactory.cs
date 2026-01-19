using lab21v1.Strategies;

namespace lab21v1.Factory
{
    public static class TaxiStrategyFactory
    {
        public static ITaxiStrategy CreateStrategy(string rideType)
        {
            // для нормалізації вводу
            rideType = (rideType ?? "").Trim().ToLower();

            return rideType switch
            {
                "economy" => new EconomyTaxiStrategy(),
                "standard" => new StandardTaxiStrategy(),
                "premium" => new PremiumTaxiStrategy(),

                // демонстрація OCP додавання нового класу
                "night" => new NightTaxiStrategy(),

                _ => throw new ArgumentException(
                        "Невідомий тип поїздки. Доступні: Economy, Standard, Premium, Night"
                     )
            };
        }
    }
}
