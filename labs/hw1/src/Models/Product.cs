namespace hw1.Generics.Models
{
    // це друга сутність (перша — сам Cache<T>), звʼязані композицією
    public class Product
    {
        // назва товару. За замовчуванням порожній рядок, щоб уникнути null.
        public string Name { get; set; } = "";
        // ціна у гривнях. Простий int для наочності.
        public int Price { get; set; }

        // перевизначення ToString для виводу в консоль.
        public override string ToString() => $"{Name} ({Price} грн)";
    }
}
