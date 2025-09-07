using System;
namespace lab1v11
{
    class Plane //літак
    {    //   Поля
        private string airline; //авіакомпанія
        private string model; //модель літака
        //      Властивість
        public int Capacity { get; set; }  // кількість місць
        //      Конструктор
        public Plane(string airlane, string madel, int capacity)
        {
            this.airline = airlane;
            this.model = model;
            this.Capacity = capacity;
            Console.WriteLine($"Створено літак {airlane} {model}, місткість: {capacity}");
        }
        //     Деструктор
        ~Plane()
        {
            Console.WriteLine($"Об'єкт літака {airline} {model} знищено.");
        }
        //     Метод
        public void Fly()
        {
            Console.WriteLine($"Літак {airline} {model} з {Capacity} місцями здійснив політ.");
        }
        //     Дод ат.Метод для виводу інформації
        public void PrintInfo()
        {
            Console.WriteLine($"Авіакомпанія: {airline}, Модель: {model}, Місткість: {Capacity}");
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            //   Створення об'єктів
            Plane p1 = new Plane("MAY", "Boeing 737", 180);
            Plane p2 = new Plane("SkyUp", "Airbus A320", 200);

            //   Виклик методів
            p1.Fly();
            p2.Fly();

            //   Виведення інформації
            p1.PrintInfo();
            p2.PrintInfo();
            Console.ReadKey();
        }
    }
}