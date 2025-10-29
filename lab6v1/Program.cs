using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab6v1
{
    // Модель предметної області
    public class Product
    {
        public string Name { get; set; }      // назва товару
        public decimal Price { get; set; }    // ціна товару
        public string Category { get; set; }  // категорія товару

        public override string ToString() => $"{Name} | {Category} | {Price:F2} грн";
    }

    // власний делегат
    public delegate decimal BinaryOp(decimal x, decimal y);

    internal class Program
    {
        // метод який приймає власний делегат
        private static decimal Apply(BinaryOp op, decimal a, decimal b)
        {
            // викликання делегату як метода
            return op(a, b);
        }

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // вихідні дані  List<T>
            var products = new List<Product>
            {
                new Product { Name = "Ноутбук Asus TUF",  Price = 38000, Category = "Електроніка" },
                new Product { Name = "Смартфон Samsung S24", Price = 22500, Category = "Електроніка" },
                new Product { Name = "Мишка Logitech",   Price = 903,   Category = "Периферія"  },
                new Product { Name = "Кавоварка Philips EP3347",Price = 27000,  Category = "Побутова"   },
                new Product { Name = "Навушники Sony WH-720",   Price = 1590,  Category = "Периферія"  },
                new Product { Name = "Монітор LG 27",       Price = 9890,  Category = "Електроніка"},
            };

            // вбудовані делегати. делегат Predicate<Product
            Predicate<Product> isExpensive = p => p.Price >= 10000m;

            // делегат Func<Product, decimal>] ціни
            Func<Product, decimal> getPrice = p => p.Price;

            // делегат Action<Product> без повернення значення
            Action<Product> print = p => Console.WriteLine(p);

            // фільтрація за ціною 
            Console.WriteLine("1) Товари дорожчі або дорівнюють 10000 грн:");
            var expensive = products.Where(p => isExpensive(p)); // використання Predicate усередині Where
            foreach (var p in expensive) print(p);

            // пошук найдорожчого товару
            Console.WriteLine("\n2) Найдорожчий товар:");
            var mostExpensive = products.OrderByDescending(getPrice).First();
            Console.WriteLine(mostExpensive);


            // середня вартість товару
            Console.WriteLine("\n3) Середня ціна всіх товарів:");
            var avg = products.Average(getPrice); // Average чекає Func<T,decimal>
            Console.WriteLine($"{avg:F2} грн");

            // додаткова LINQ операція
            var sumByAggregate = products.Aggregate(0m, (acc, p) => acc + p.Price);
            var avgByAggregate = sumByAggregate / products.Count;
            Console.WriteLine($"(через Aggregate) {avgByAggregate:F2} грн");

            // інші лямбда-приклади 
            Console.WriteLine("\n4) Вивід лише назв, відсортованих за зростанням ціни:");
            var namesAsc = products
                .OrderBy(getPrice)           // Func<Product, decimal>
                .Select(p => p.Name);        // Func<Product, string>
            Console.WriteLine(string.Join(", ", namesAsc));

            // анонімні методи + власний делегат
            BinaryOp addAnon = delegate(decimal x, decimal y) { return x + y; };

            // лямбда вираз
            BinaryOp mulLambda = (x, y) => x * y;

            Console.WriteLine("\n5) Власний делегат BinaryOp (анонімний метод і лямбда):");
            Console.WriteLine($"  10 + 5 = {Apply(addAnon, 10, 5)}");
            Console.WriteLine($"  10 * 5 = {Apply(mulLambda, 10, 5)}");

            // комбіноване використання: фільтр + дія друку
            Console.WriteLine("\n6) Друк периферії дешевше 3000 грн:");
            var cheapPeripherals = products
                .Where(p => p.Category == "Периферія" && p.Price < 3000m); // лямбда-предикат

            foreach (var p in cheapPeripherals)
                print(p); // Action<Product>

            // це для бонуса
            var notifier = new PriceNotifier(12000m);
            notifier.OnOverpriced += p => Console.WriteLine($"[Подія] Дорогий товар! {p}");
            foreach (var p in products) notifier.Check(p);

            Console.WriteLine("\nГотово.");
        }
    }

    // і це для бонуса
    public class PriceNotifier
    {
        private readonly decimal _limit;

        // оголошення події через Action<Product>
        public event Action<Product>? OnOverpriced;

        public PriceNotifier(decimal limit) => _limit = limit;

        public void Check(Product p)
        {
            if (p.Price >= _limit)
                OnOverpriced?.Invoke(p); // виклик делегата-передплатника
        }
    }
}