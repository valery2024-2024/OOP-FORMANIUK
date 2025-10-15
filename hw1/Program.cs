using System;
using hw1.Generics.Core;
using hw1.Generics.Core.Comparers;
using hw1.Generics.Models;

namespace hw1.Generics
{
    class Program
    {
        static void Main()
        {
            try
            {
                // створення кешу з кількістю сім
                var cache = new Cache<Product>(capacity: 7);

                // додавання елементів (7-й елемент витіснить найстаріший)
                cache.Add(new Product { Name = "Молоток",  Price = 250 });
                cache.Add(new Product { Name = "Викрутка", Price = 100 });
                cache.Add(new Product { Name = "Пилка", Price = 180 });
                cache.Add(new Product { Name = "Рулетка", Price = 280 });
                cache.Add(new Product { Name = "Рівень", Price = 380 });
                cache.Add(new Product { Name = "Лопата", Price = 50 });
                cache.Add(new Product { Name = "Ключ",     Price = 220 }); // витіснить "Молоток"

                Console.WriteLine("— Перед сортуванням:");
                foreach (var p in cache.All()) Console.WriteLine("  " + p);

                // сортування методом бульбашки за ціною
                var cmp = new ProductPriceComparer();
                cache.BubbleSort(cmp);

                Console.WriteLine("\n— Після Bubble Sort ( ціна):");
                foreach (var p in cache.All()) Console.WriteLine("  " + p);

                // обчислення з колекцією (без LINQ): мін/макс, середнє
                var min = cache.Min(cmp);
                var max = cache.Max(cmp);
                Console.WriteLine($"\nМінімальна ціна: {min}");
                Console.WriteLine($"Максимальна ціна: {max}");

                // Середнє вручну
                int sum = 0; var all = cache.All();
                for (int i = 0; i < all.Count; i++) sum += all[i].Price;
                double avg = all.Count == 0 ? 0 : (double)sum / all.Count;
                Console.WriteLine($"Середня ціна: {avg:F2} грн");

                // приклад пошуку та видалення без LINQ
                var found = cache.Find(p => p.Name == "Ключ");
                Console.WriteLine($"\nЗнайшов: {found}");
                var removed = cache.Remove(p => p.Name == "Ключ");
                Console.WriteLine($"Видалено 'Ключ': {removed}");

                Console.WriteLine("\n— Стан кешу наприкінці:");
                foreach (var p in cache.All()) Console.WriteLine("  " + p);
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine("Помилка введення: " + ex.Message);
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine("Не знайдено: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Непередбачена помилка: " + ex.Message);
            }
        }
    }
}
