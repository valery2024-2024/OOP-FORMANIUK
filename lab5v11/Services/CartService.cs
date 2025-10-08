using System.Collections.Generic;
using System.Linq;
using lab5v11.Data;
using lab5v11.Models;
using lab5v11.Exceptions;

namespace lab5v11.Services
{
    // Клас, який відповідає за роботу з кошиком та розрахунки
    public class CartService
    {
        private readonly IRepository<PriceItem> _prices;

        // У конструктор передаємо репозиторій прайсу (через залежність)
        public CartService(IRepository<PriceItem> prices)
        {
            _prices = prices;
        }

        // Отримати ціну за кодом товару (з перевіркою існування)
        public decimal GetUnitPrice(string code)
        {
            var item = _prices.Find(p => p.Code == code);   // Якщо не знайдено — буде виняток
            return item.Price;
        }

        // Підрахунок загальної суми: кількість * ціна для кожної позиції
        public decimal Subtotal(IEnumerable<CartItem> cart)
        {
            // сума кількість * ціна (LINQ + агрегація)
            return cart.Sum(c => GetUnitPrice(c.Code) * c.Quantity);
        }

        // Застосування знижки купона у відсотках
        public decimal ApplyCoupon(decimal subtotal, decimal percent) // percent = 7 => -7%
            => subtotal - subtotal * (percent / 100m);

        // Середня ціна за одну унікальну позицію у кошику
        public decimal AverageUnitPricePerPosition(IEnumerable<CartItem> cart)
        {
            var codes = cart.Select(c => c.Code).Distinct().ToList();  // Унікальні коди
            var prices = codes.Select(GetUnitPrice);  // Отримуємо ціни
            return prices.Average();  // Обчислюємо середнє
        }

        // Знайти товари дорожчі за певну ціну приклад LINQ-запиту
        public IEnumerable<PriceItem> Expensive(decimal threshold)
            => _prices.Where(p => p.Price > threshold);
    }
}
