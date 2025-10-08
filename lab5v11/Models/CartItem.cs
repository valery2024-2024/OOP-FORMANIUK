namespace lab5v11.Models
{
    // Клас для елементів кошика
    // Кожен елемент містить код товару (агрегація з PriceItem) і кількість
    public class CartItem
    {
        public string Code { get; }  // Код товару (посилання на PriceItem)
        public int Quantity { get; private set; }  // Кількість одиниць

        public CartItem(string code, int quantity)
        {
            Code = code;
            Quantity = quantity;
        }

        // Метод для додавання ще одиниць товару до наявного
        public void Add(int qty) => Quantity += qty;
        public override string ToString() => $"{Code} x {Quantity}";
    }
}
