using System;

namespace lab20
{
    public class OrderService
    {
        private readonly IOrderValidator _validator;
        private readonly IOrderRepository _repository;
        private readonly IEmailService _emailService;

        public OrderService(IOrderValidator validator, IOrderRepository repository, IEmailService emailService)
        {
            _validator = validator;
            _repository = repository;
            _emailService = emailService;
        }

        public void ProcessOrder(Order order)
        {
            Console.WriteLine("OrderService: початок обробки");

            // валідація
            if (!_validator.IsValid(order))
            {
                Console.WriteLine("Помилка: замовлення невалідне (TotalAmount має бути > 0)");
                order.Status = OrderStatus.Cancelled;
                return;
            }

            order.Status = OrderStatus.PendingValidation;
            Console.WriteLine("Валідація пройдена");

            // pбереження
            _repository.Save(order);

            // Email
            _emailService.SendOrderConfirmation(order);

            // cтатус
            order.Status = OrderStatus.Processed;
            Console.WriteLine($"Статус замовлення оновлено на: {order.Status}");

            Console.WriteLine("OrderService: обробку завершено");
        }
    }
}
