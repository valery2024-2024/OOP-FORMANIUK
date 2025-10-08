using System;
using System.Collections.Generic;
using lab5v11.Data;
using lab5v11.Models;
using lab5v11.Services;
using lab5v11.Exceptions;

namespace lab5v11
{
    internal class Program
    {
        static void Main()
        {
            // Для роскодування українських символів щоб відображались у консолі
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Лабораторна №5 — Generics, Колекції, LINQ, Винятки (варіант 11)\n");

            // Створення Прайс-листу (Repository<PriceItem>)
            IRepository<PriceItem> priceRepo = new Repository<PriceItem>();
            // Додаємо товари до репозиторію
            priceRepo.Add(new PriceItem("P001", "Масло моторне 5W-30", 899m));
            priceRepo.Add(new PriceItem("P002", "Фільтр повітряний", 320m));
            priceRepo.Add(new PriceItem("P003", "Гальмівні колодки", 1450m));
            priceRepo.Add(new PriceItem("P004", "Антифриз 5л", 520m));
            priceRepo.Add(new PriceItem("P005", "Щітки склоочисника", 280m));

            Console.WriteLine("Прайс-лист:");
            foreach (var p in priceRepo.All())
                Console.WriteLine("  " + p);
            Console.WriteLine();

            // Створення кошика CartItem агрегація: елементи кошика посилаються на коди з прайсу
            var cart = new List<CartItem>
            {
                new CartItem("P001", 1),
                new CartItem("P003", 1),
                new CartItem("P005", 2),
                new CartItem("BAD", 1) // Некоректний код — для перевірки винятку
            };

            // Створюємо CartService і передаємо прайс
            var service = new CartService(priceRepo);

            try
            {
                // Розрахунок проміжної суми (Subtotal)
                var subtotal = service.Subtotal(cart); 
                // Застосування знижки (7%)
                var totalWithCoupon = service.ApplyCoupon(subtotal, percent: 7m);
                // Середня ціна за позицію
                var avgPrice = service.AverageUnitPricePerPosition(cart);

                // Вивід результатів
                Console.WriteLine($"Проміжна сума: {subtotal:F2} грн");
                Console.WriteLine($"Після знижки 7%: {totalWithCoupon:F2} грн");
                Console.WriteLine($"Середня ціна за позицію: {avgPrice:F2} грн");
            }
            catch (NotFoundException ex)
            {
                // Обробка власного винятку, якщо не знайдено товар
                Console.WriteLine($"[Помилка] {ex.Message}");
                // Видаляємо некоректний товар і перерахуємо заново
                cart.RemoveAll(c => c.Code == "BAD");

                var subtotal = service.Subtotal(cart);
                var totalWithCoupon = service.ApplyCoupon(subtotal, 7m);
                var avgPrice = service.AverageUnitPricePerPosition(cart);

                Console.WriteLine("\nПісля виправлення кошика:");
                Console.WriteLine($"Проміжна сума: {subtotal:F2} грн");
                Console.WriteLine($"Після знижки 7%: {totalWithCoupon:F2} грн");
                Console.WriteLine($"Середня ціна за позицію: {avgPrice:F2} грн");
            }

            // Приклад LINQ-запиту до репозиторію
            Console.WriteLine("\nПозиції дорожчі за 500 грн:");
            foreach (var p in service.Expensive(500m))
                Console.WriteLine("  " + p);

            Console.WriteLine("\nГотово.");
        }
    }
}
