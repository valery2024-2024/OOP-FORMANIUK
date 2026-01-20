using lab21v1.Factory;
using lab21v1.Services;
using lab21v1.Strategies;

namespace lab21v1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("Доступні типи поїздки: Economy, Standard, Premium, Night");

                Console.Write("\nВведіть тип поїздки: ");
                string rideType = Console.ReadLine() ?? "";

                decimal distanceKm;
                decimal waitingMin;

                if (!TryReadDecimal("Введіть відстань (км): ", out distanceKm))
                {
                    if (!AskToContinue()) break;
                    continue;
                }

                if (!TryReadDecimal("Введіть час простою (хв): ", out waitingMin))
                {
                    if (!AskToContinue()) break;
                    continue;
                }

                try
                {
                    ITaxiStrategy strategy = TaxiStrategyFactory.CreateStrategy(rideType);
                    TaxiService service = new TaxiService();

                    decimal cost = service.CalculateRideCost(distanceKm, waitingMin, strategy);

                    Console.WriteLine($"\nТип поїздки: {rideType}");
                    Console.WriteLine($"Відстань: {distanceKm} км");
                    Console.WriteLine($"Простій: {waitingMin} хв");
                    Console.WriteLine($"Вартість: {cost} грн");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("\nПОМИЛКА: " + ex.Message);
                }

                if (!AskToContinue())
                    break;
            }

            Console.WriteLine("Роботу завершено. Натисніть Enter для виходу...");
            Console.ReadLine();
        }

        // Спроба зчитати число
        static bool TryReadDecimal(string prompt, out decimal value)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? "";

            input = input.Replace(',', '.');

            if (!decimal.TryParse(input,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out value) || value < 0)
            {
                Console.WriteLine("Помилка вводу. Введіть додатне число.");
                return false;
            }

            return true;
        }

        // Питання: продовжити чи вийти
        static bool AskToContinue()
        {
            while (true)
            {
                Console.WriteLine("\nОберіть дію:");
                Console.WriteLine("1 — Продовжити");
                Console.WriteLine("0 — Вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine() ?? "";

                if (choice == "1")
                    return true;

                if (choice == "0")
                    return false;

                Console.WriteLine("Невірний вибір. Введіть 1 або 0.");
            }
        }
    }
}
