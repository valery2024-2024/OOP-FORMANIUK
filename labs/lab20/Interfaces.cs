namespace lab20
{
    public interface IOrderValidator
    {
        bool IsValid(Order order);
    }
    public interface IOrderRepository
    {
        void Save(Order order);
        Order? GetById(int id);
    }
    public interface IEmailService
    {
        void SendOrderConfirmation(Order order);
    }
}