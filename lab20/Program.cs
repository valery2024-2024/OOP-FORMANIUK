using System;

namespace lab20
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Демонстрація поганого класу, що порушує SRP
            Console.WriteLine("1) Початковий варіант (OrderProcessor - порушує SRP)");
            var processor = new OrderProcessor();

            var badValidOrder = new Order(1, "Іван", 2500);
            var badInvalidOrder = new Order(2, "Петро", -10);

            processor.ProcessOrder(badValidOrder);
            Console.WriteLine($"Підсумковий статус: {badValidOrder.Status}\n");

            processor.ProcessOrder(badInvalidOrder);
            Console.WriteLine($"Підсумковий статус: {badInvalidOrder.Status}\n");


            // 2) Демонстрація SRP-рефакторингу через OrderService + інтерфейси
            Console.WriteLine("2) Рефакторинг SRP (OrderService + інтерфейси + DI)");

            IOrderValidator validator = new SimpleOrderValidator();
            IOrderRepository repository = new InMemoryOrderRepository();
            IEmailService emailService = new ConsoleEmailService();

            var orderService = new OrderService(validator, repository, emailService);

            var validOrder = new Order(3, "Олег", 1500);
            var invalidOrder = new Order(4, "Сергій", 0);

            // Валідне замовлення
            orderService.ProcessOrder(validOrder);
            Console.WriteLine($"Підсумковий статус: {validOrder.Status}\n");

            // Невалідне замовлення
            orderService.ProcessOrder(invalidOrder);
            Console.WriteLine($"Підсумковий статус: {invalidOrder.Status}\n");

            // Перевірка GetById
            var loaded = repository.GetById(3);
            if (loaded != null)
            {
                Console.WriteLine($"[GetById] Знайдено замовлення #{loaded.Id}, клієнт: {loaded.CustomerName}, сума: {loaded.TotalAmount}, статус: {loaded.Status}");
            }
            else
            {
                Console.WriteLine("[GetById] Замовлення не знайдено.");
            }
        }
    }
}
