namespace lab5v11.Models
{
    // Клас для зберігання інформації про товар у прайсі
    public class PriceItem
    {
        public string Code { get; }  // Код товару (унікальний ідентифікатор)
        public string Name { get; }  // Назва товару
        public decimal Price { get; }  // Ціна товару

        public PriceItem(string code, string name, decimal price)
        {
            Code = code;
            Name = name;
            Price = price;
        }

        // Перевизначення ToString для зручного відображення у консолі
        public override string ToString() => $"{Code} | {Name} | {Price:F2} грн";
    }
}
