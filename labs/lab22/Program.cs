using System;
using System.Collections;

namespace lab22
{
    // ієрархія де порушено LSP
    // базовий клас прямокутник
    class Rectangle
    {
        public virtual int Width { get; set;}
        public virtual int Height { get; set; }

          public int Area()
        {
            return Width * Height;
        }
    }

    class Square : Rectangle
    {
        private int _side;

        public override int Width
        {
            get { return _side;}
            set
            {
                _side = value;
                base.Width = value;
                base.Height = value;
            }
        }

        public override int Height
        {
            get { return _side; }
            set
            {
                _side = value;
                base.Width = value;
                base.Height = value;
            }
        }
    }

    interface IShape
    {
        int Area();
    }

    class GoodRectangle : IShape
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public int Area() => Width * Height;
    }

    class GoodSquare : IShape
    {
        public int Side { get; set; }

        public int Area() => Side * Side;
    }

    class Program
    {
        // клієнтський метод працює з базовим типом прямокутник(Rectangle) очікує що ширину і висоту можна змінити незалежно.
        static void ClientCodeBad(Rectangle r)
        {
             r.Width = 5;
             r.Height = 10;

             // очікування Area = 5 * 10 = 50

             Console.WriteLine($"Очікувана площа: 50");
             Console.WriteLine($"Фактична площа: {r.Area()}");
        }

        static void ClientCodeGood(IShape shape)
        {
            Console.WriteLine($"Площа фігури: {shape.Area()}");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Порушення LSP погана ієрархія");

            Console.WriteLine("Передає Rectangle:");
            ClientCodeBad(new Rectangle());

            Console.WriteLine();

            Console.WriteLine("Передає Square як Rectangle:");
            ClientCodeBad(new Square());

            Console.WriteLine();
            Console.WriteLine("дотримання LSP зміна ієрархії");

            GoodRectangle gr = new GoodRectangle { Width = 5, Height = 10};
            GoodSquare gs = new GoodSquare { Side = 5};

            Console.WriteLine("Передає GoodRectangle:");
            ClientCodeGood(gr);

            Console.WriteLine("Передає GoodSquare:");
            ClientCodeGood(gs);

            Console.WriteLine();
            Console.WriteLine("Натисніть Enter...");
            Console.ReadLine();
        }
    }
} 