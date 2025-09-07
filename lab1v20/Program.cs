using System;
namespace lab1v20
{
    class Figure
    {
        private string name;
        public double Area { get; set; }
        public Figure(string name, double area)
        {
            this.name = name;
            Area = area;
            Console.WriteLine($"Створено фігуру: {name}, площа {area}");
        }
        ~Figure()
        {
            Console.WriteLine($"Об'єкт Figure \"{name}\"знищено.");
        }
        public string GetFigure()
        {
            return $"Назва: {name}, Площа = {Area}";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Figure f1 = new Figure("Квадрат", 25.0);
            Figure f2 = new Figure("Коло", 78.5);
            Figure f3 = new Figure("Трикутник", 30.2);
            Console.WriteLine();

            Console.WriteLine(f1.GetFigure());
            Console.WriteLine(f2.GetFigure());
            Console.WriteLine(f3.GetFigure());
            Console.WriteLine();
            f1.Area = 36.0;
            Console.WriteLine("Після зміни:");
            Console.WriteLine(f1.GetFigure());
            Console.WriteLine("\nНатисніть будь-яку клавішу для завершення...");
            Console.ReadKey();
        }
    }
}