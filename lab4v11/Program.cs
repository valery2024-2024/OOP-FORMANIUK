using System;
using System.Collections.Generic;
using System.Linq;

namespace lab4v11
{
    // 1) Інтерфейс для товарів
    public interface IProduct
    {
        string Name { get; }          // назва товару
        decimal UnitPrice { get; }    // ціна за одиницю
        int Quantity { get; }         // кількість
        decimal TotalPrice();         // загальна вартість (UnitPrice * Quantity)
        string Describe();            // опис для виводу
    }

    // 2) Абстрактний базовий клас, спільна логіка
    public abstract class ProductBase : IProduct
    {
        public string Name { get; protected set; }
        public decimal UnitPrice { get; protected set; }
        public int Quantity { get; protected set; }

        // Конструктор з перевірки вхідних даних
        protected ProductBase(string name, decimal unitPrice, int quantity)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required", nameof(name));
            if (unitPrice < 0) throw new ArgumentOutOfRangeException(nameof(unitPrice), "UnitPrice cannot be negative");
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive");

            Name = name;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        // загальна вартість   ціна * кількість
        public virtual decimal TotalPrice() => UnitPrice * Quantity;

        // метод Describe залишаю абстрактним
        public abstract string Describe();
    }

    // 3) Реалізація 1 Food (їжа)
    public class Food : ProductBase
    {
        public DateTime ExpirationDate { get; } // термін придатності
        public bool IsPerishable => ExpirationDate.Date <= DateTime.Today.AddDays(3);

        public Food(string name, decimal unitPrice, int quantity, DateTime expirationDate)
            : base(name, unitPrice, quantity)
        {
            ExpirationDate = expirationDate;
        }
        // перевизначений метод Describe  опис їжі

        public override string Describe()
        {
            return $"[Food] {Name}: {Quantity} шт × {UnitPrice:C} = {TotalPrice():C}; " +
                   $"придатно до {ExpirationDate:yyyy-MM-dd}" +
                   (IsPerishable ? " (швидкопсувний)" : "");
        }
    }

    // 4) Реалізація 2  Clothes (одяг)
    public class Clothes : ProductBase
    {
        public string Size { get; }  // розмір (S, M, L, XL,)
        public string Category { get; } // категорія (футболка, джинси)

        public Clothes(string name, decimal unitPrice, int quantity, string size, string category)
            : base(name, unitPrice, quantity)
        {
            Size = size;
            Category = category;
        }

        // перевизначений метод Describe   опис одягу
        public override string Describe()
        {
            return $"[Clothes] {Name} ({Category}, розмір {Size}): " +
                   $"{Quantity} шт × {UnitPrice:C} = {TotalPrice():C}";
        }
    }

    // 5) Композиція  клас Cart (кошик володіє товарами)
    public class Cart
    {
        // приватний список товарів
        private readonly List<IProduct> _items = new();
        // читання списку ззовні
        public IReadOnlyList<IProduct> Items => _items;
        // додавання товару
        public void Add(IProduct product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            _items.Add(product);
        }
        // видалення товару за індексом
        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= _items.Count) return false;
            _items.RemoveAt(index);
            return true;
        }

        // сума замовлення та вартості всіх товарів
        public decimal TotalAmount() => _items.Sum(p => p.TotalPrice());

        // середня ціна за одиницю товару
        public decimal AverageUnitPrice()
        {
            int totalUnits = _items.Sum(p => p.Quantity);
            if (totalUnits == 0) return 0m;
            return _items.Sum(p => p.UnitPrice * p.Quantity) / totalUnits;
        }

        // формує рядок зі списком товарів
        public override string ToString()
        {
            if (_items.Count == 0) return "(кошик порожній)";
            return string.Join(Environment.NewLine, _items.Select((p, i) => $"{i + 1}. {p.Describe()}"));
        }
    }

    // 6) Агрегація  замовник та замовлення
    public class Customer
    {
        public string FullName { get; }

        // конструктор з параметром
        public Customer(string fullName) => FullName = fullName;
    }

    public class Order
    {
        public Customer Customer { get; } // замовник
        public Cart Cart { get; } // кошик
        // конструктор з агрегацією  використовує готовий Cart i Customer

        public Order(Customer customer, Cart cart)
        {
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            Cart = cart ?? throw new ArgumentNullException(nameof(cart));
        }

        // загальна сума замовлення
        public decimal Amount => Cart.TotalAmount();

        // замовлення виставляється в рядок
        public override string ToString()
        {
            return $"Замовник: {Customer.FullName}\n" +
                   $"Товари:\n{Cart}\n" +
                   $"- Сума замовлення: {Amount:C}\n" +
                   $"- Середня ціна за одиницю: {Cart.AverageUnitPrice():C}";
        }
    }

    // 7) Демонстрація  основна програма
    internal class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // товари двох типів
            var bread = new Food(name: "Хліб цільнозерновий", unitPrice: 22.50m, quantity: 2,
                                 expirationDate: DateTime.Today.AddDays(2));
            var milk  = new Food(name: "Молоко 2.5%", unitPrice: 34.90m, quantity: 1,
                                 expirationDate: DateTime.Today.AddDays(5));

            var tshirt = new Clothes(name: "Футболка Adidas", unitPrice: 455.00m, quantity: 1,
                                     size: "XL", category: "Футболка");
            var jeans  = new Clothes(name: "Джинси Wrangler", unitPrice: 2487.00m, quantity: 1,
                                     size: "46", category: "Штани");

            // композиція  кошик володіє списком товарів
            var cart = new Cart();
            cart.Add(bread);
            cart.Add(milk);
            cart.Add(tshirt);
            cart.Add(jeans);

            // агрегація  замовлення використовує Customer(клієнт) і Cart(кошик)
            var customer = new Customer("Валерій Форманюк");
            var order = new Order(customer, cart);

            // вивід результатів
            Console.WriteLine("Лабораторна робота №4 — Варіант №11 — Кошик товарів");
            Console.WriteLine(order);

            Console.WriteLine("\nНатисніть Enter, щоб завершити...");
            Console.ReadLine();
        }
    }
}
