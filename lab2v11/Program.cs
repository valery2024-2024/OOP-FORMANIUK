using System;
namespace lab2v11
{
    // створенння класу Temperature в якому буде:
    //  інкапсуляція, властивості, індексатор та оператори
    public class Temperature
    {
        //  приватне поле - інкапсуляція
        private double celsius;
        //  властивість для доступу в Цельсіях
        public double Celsius
        {
            get => celsius;
            set
            {
                if (value < -270.00)
                    throw new ArgumentOutOfRangeException(nameof(Celsius),
                     "Температура не може бути нижча за -270.00 °C");
                celsius = value;
            }

        }
        //  додаткова обчислювана властивість Фаренгейт 
        public double Fahrengeit
        {
            get => celsius * 9 / 5 + 32;
            set => Celsius = (value - 32) * 5 / 9;
        }
        //   створення індексатора this (C або F) 
        //   дозволяє читати або ж записувати температуру
        public double this [string unit]
        {
            get
            {
                switch (unit?.Trim().ToUpperInvariant())
                {
                    case "C": return Celsius;
                    case "F": return Fahrengeit;
                    default: throw new ArgumentException("Використовуйте \"C\" або \"F\".",
                    nameof(unit));
                }
            }
            set
            {
                switch (unit?.Trim().ToUpperInvariant())
                {
                    case "C": Celsius = value; break;
                    case "F": Fahrengeit = value; break;
                    default: throw new ArgumentException("Використовуйте \"C\" або \"F\".",
                    nameof(unit));

                }

            }

        }
        //   Створення конструкторів
        public Temperature() : this(0) { } //  за замовчуванням 0°C
        public Temperature(double celsius) => Celsius = celsius;
        //  перевантаження операторів >, <, ==, і логічна пара
        public static bool operator >(Temperature a, Temperature b)
        {
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                throw new ArgumentNullException("Порівнювані об'єкти не можуть бути null.");
            return a.Celsius > b.Celsius;
        }
        public static bool operator <(Temperature a, Temperature b)
        {
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                throw new ArgumentNullException("Порівнювані об'єкти не можуть бути null.");
            return a.Celsius <
             b.Celsius;
        }
        public static bool operator ==(Temperature a, Temperature b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return Math.Abs(a.Celsius - b.Celsius) < 1e-9; // допускається крихітна похибка double
        }
        public static bool operator !=(Temperature a, Temperature b) => !(a == b);
        //  коли перевантажується ==, треба також перевизначити Equals i GetHashCode
        public override bool Equals(object? obj) => obj is Temperature t && this == t;
        public override int GetHashCode() => Celsius.GetHashCode();
        public override string ToString() => $"{Celsius:F1} °C ({Fahrengeit:F1} °F)";
    }
    //  точка входу в програму
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var t1 = new Temperature(25); // 25°C
            var t2 = new Temperature();  //  0°C
            t2.Fahrengeit = 77;         // через set у °F -> 25°C

            Console.WriteLine("Властивості (Celsius / Fahrengei)");
            Console.WriteLine($"t1: C={t1.Celsius:F1}, F={t1.Fahrengeit:F1}");
            Console.WriteLine($"t2: C={t2.Celsius:F1}, F={t2.Fahrengeit:F1}");

            Console.WriteLine("\nIндeкcaтop this [\"C\"/\"F\"]");
            Console.WriteLine($"t1 [\"C\"] = {t1 ["C"]:F1}");
            Console.WriteLine($"t1 [\"F\"] = {t1 ["F"]:F1}");
            t1["F"] = 32; // встановив через індексатор у °F -> стане 0°C
            Console.WriteLine($"Після t1 [\"F\"] = 32 -> t1 = {t1}");

            Console.WriteLine("\nПepeвaнтaжeнi оператори >, <, == ");
            Console.WriteLine($"t1 == t2 ? {(t1 == t2)}");
            Console.WriteLine($"t1 > t2 ? {(t1 > t2)}");
            Console.WriteLine($"t1 < t2 ? {(t1 < t2)}");
            Console.WriteLine($"t1 != t2 ? {(t1 != t2)}");

            // приклад з масивом та індексатором  
            var arr = new Temperature[]
            {
                new Temperature(10),
                new Temperature(0),
                new Temperature(36.6)
            };
            Console.WriteLine("\nГoтoвo. Натисніть будь-яку клавішу щоб завершити...");
            Console.ReadLine();
        }
    }
}
