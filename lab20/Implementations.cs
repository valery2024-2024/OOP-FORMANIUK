using System;
using System.Collections.Generic;

namespace lab20
{
    public class SimpleOrderValidator : IOrderValidator
    {
        public bool IsValid(Order order)
        {
            return order.TotalAmount > 0;
        }
    }

    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly Dictionary<int, Order> _storage = new();

        public void Save(Order order)
        {
            _storage[order.Id] = order;
            Console.WriteLine($"[Repo] Замовлення #{order.Id} збережено в пам'ять (імітація БД).");
        }

        public Order? GetById(int id)
        {
            _storage.TryGetValue(id, out var order);
            return order;
        }
    }

    public class ConsoleEmailService : IEmailService
    {
        public void SendOrderConfirmation(Order order)
        {
            Console.WriteLine($"[Email] Надіслано підтвердження замовлення #{order.Id} клієнту {order.CustomerName} (імітація).");
        }
    }
}
