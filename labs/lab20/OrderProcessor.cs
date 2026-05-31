using System;

namespace lab20
{
    // початковий клас який порушує SRP та робить все одразу
    public class OrderProcessor
    {
        public void ProcessOrder(Order order)
        {
            Console.WriteLine("OrderProcessor: початок обробки замовлення");

            // валідація
            if (order.TotalAmount <= 0)
            {
                Console.WriteLine("Помилка: сума замовлення має бути більше 0");
                order.Status = OrderStatus.Cancelled;
                return;
            }

            order.Status = OrderStatus.PendingValidation;
            Console.WriteLine("Валідація пройдена");

            // збереження в базу
            Console.WriteLine($"Збереження замовлення #{order.Id} в базу даних (імітація)");

            // Email-сповіщення
            Console.WriteLine($"Відправка email клієнту {order.CustomerName} (імітація)");

            // оновлення статусу
            order.Status = OrderStatus.Processed;
            Console.WriteLine($"Статус замовлення оновлено на: {order.Status}");

            Console.WriteLine("OrderProcessor: обробку завершено");
        }
    }
}
