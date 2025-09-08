using System;
namespace lab1v20
{
    class Figure // Фігура
    {
        //   Поля
        private string name; // ім'я (наприклад квадрат або будь яка інша фігура)
        //   Властивість
        public double Area { get; set; } //  площа
        //   Конструктор
        public Figure(string name, double area)
        {
            this.name = name;
            Area = area;
            Console.WriteLine($"Створено фігуру: {name}, площа {area}");
        }
        //   Деструктор
        ~Figure()
        {
            Console.WriteLine($"Об'єкт Figure \"{name}\"знищено.");
        }
        //   Метод 
        public string GetFigure()
        {
            return $"Назва: {name}, Площа = {Area}";
        }
    }
    //   Точка входу в програму
    class Program
    {
        static void Main(string[] args)
        {
            //   Створення об'єктів
            Figure f1 = new Figure("Квадрат", 25.0);
            Figure f2 = new Figure("Коло", 78.5);
            Figure f3 = new Figure("Трикутник", 30.2);
            Console.WriteLine();

            //   Виклик методів
            Console.WriteLine(f1.GetFigure());
            Console.WriteLine(f2.GetFigure());
            Console.WriteLine(f3.GetFigure());
            Console.WriteLine();

            //   Зміна властивості
            f1.Area = 36.0;
            Console.WriteLine("Після зміни:");
            Console.WriteLine(f1.GetFigure());

            //   Завершення програми
            Console.WriteLine("\nHaтиcнiть будь-яку клавішу для завершення...");
            Console.ReadKey();
        }
    }
}