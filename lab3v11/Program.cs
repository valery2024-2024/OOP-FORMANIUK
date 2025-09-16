using System;
using System.Collections.Generic;
namespace lab3v11
{
    // базовий клас
    class Instrument
    {
        // protected - доступно в похідних; інкапсульоване від зовнішнього коду
        protected string name;

        // середня тривалість однієї композиціх (хв.)
        private double avgPieceMinutes;

        // публічні властивості з доступом до інкапсульованих полів.
        public string Name
        {
            get => name;
            protected set => name = value ?? "Unknow";
        }

        public double AvgPieceMinutes
        {
            get => avgPieceMinutes;
            protected set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(AvgPieceMinutes), "Тривалість має бути > 0");
                avgPieceMinutes = value;
            }
        }
        // Конструктор базового класу
        public Instrument(string name, double avgPieceMinutes)
        {
            this.name = string.IsNullOrWhiteSpace(name) ? "Unknow" : name;
            AvgPieceMinutes = avgPieceMinutes;
            // Console.WriteLine($"[DEBUG] Створено {Name} (avg {AvgPieceMinutes} хв).");
        }

        // Віртуальний метод: буде перевизначений у похідних
        public virtual string Play()
        {
            return $"Інструмент {Name} виконує композицію.";
        }

        // Порахувати, скільки творів можна зіграти за concertMinutes.
        // За замовчуванням: просто ділимо на середню тривалість.
        // Може бути перевизначено в похідних (наприклад, з урахуванням пауз).
        
        public virtual int CountPiecesForConcert(double concertMinutes)
        {
            if (concertMinutes <= 0) return 0;
            return (int)Math.Floor(concertMinutes / AvgPieceMinutes);
        }

        // Деструктор — для демонстрації (у C# викликається GC)
        ~Instrument()
        {
            //Console.WriteLine($"Об'єкт {Name} знищено збирачем сміття.");
        }
    }

    // Похідний клас: Гітара
    class Guitar : Instrument
    {
        // Додатковий параметр: чи є соло (впливатиме на середній час)
        public bool HasSolo { get; }

        // base(...) — викликаємо конструктор базового класу
        public Guitar(string name, bool hasSolo)
            : base(name, avgPieceMinutes: hasSolo ? 4.0 : 3.0) // з соло — довші твори
        {
            HasSolo = hasSolo;
        }

        // Перевизначення поведінки
        public override string Play()
        {
            return HasSolo
                ? $"Гітара {Name} грає енергійне соло!"
                : $"Гітара {Name} акомпанує ритмічно.";
        }

        // Приклад, як трохи змінити розрахунок: додаємо короткі паузи (0.2 хв)
        public override int CountPiecesForConcert(double concertMinutes)
        {
            double effectiveAvg = AvgPieceMinutes + 0.2; // пауза між творами
            return (int)Math.Floor(concertMinutes / effectiveAvg);
        }
    }

    // Похідний клас: Піаніно
    class Piano : Instrument
    {
        // Додатковий параметр: чи класична програма (зазвичай довші п'єси)
        public bool IsClassical { get; }

        public Piano(string name, bool isClassical)
            : base(name, avgPieceMinutes: isClassical ? 6.0 : 4.5)
        {
            IsClassical = isClassical;
        }

        public override string Play()
        {
            return IsClassical
                ? $"Піаніно {Name} виконує класику витончено."
                : $"Піаніно {Name} грає легкі сучасні п'єси.";
        }

        // Для класики додамо ще паузу на аплодисменти (0.5 хв), інакше меншу (0.2)
        public override int CountPiecesForConcert(double concertMinutes)
        {
            double pause = IsClassical ? 0.5 : 0.2;
            double effectiveAvg = AvgPieceMinutes + pause;
            return (int)Math.Floor(concertMinutes / effectiveAvg);
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Лабораторна №3 — Наслідування (варіант 11: Музичні інструменти)");
            Console.WriteLine("Базовий клас Instrument, похідні: Guitar, Piano.\n");

            // 1) Ввід тривалості концерту (хвилини)
            double concertMinutes = ReadPositiveDouble("Введіть тривалість концерту у хвилинах (наприклад, 90): ");

            // 2) Створюємо колекцію інструментів (поліморфізм)
            //    Тип колекції — базовий: Instrument, але зберігаємо Guitar і Piano.
            List<Instrument> instruments = new List<Instrument>
            {
                new Guitar(name: "Fender Stratocaster", hasSolo: true),
                new Guitar(name: "Yamaha FG800", hasSolo: false),
                new Piano (name: "Yamaha U3", isClassical: true),
                new Piano (name: "Korg B2",     isClassical: false)
            };

            Console.WriteLine($"\nТривалість концерту: {concertMinutes} хв.\n");

            // 3) Поліморфний виклик: для кожного інструмента виконуємо Play() та свій підрахунок творів
            int totalPieces = 0;
            foreach (Instrument inst in instruments)
            {
                Console.WriteLine(inst.Play());
                int pieces = inst.CountPiecesForConcert(concertMinutes);
                totalPieces += pieces;
                Console.WriteLine($"  {inst.Name}: можна зіграти ≈ {pieces} композицій за {concertMinutes} хв.\n");
            }

            Console.WriteLine($"Загалом орієнтовно: {totalPieces} композицій усіма інструментами.\n");

            // 4) Демонстрація роботи деструктора (не обов'язково, але є у критеріях)
            //    Створимо тимчасовий об'єкт у блоці, вийдемо зі scope і попросимо GC.
            CreateAndDropTemp();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.WriteLine("Натисніть Enter, щоб завершити...");
            Console.ReadLine();
        }

        // Допоміжний метод вводу позитивного числа
        static double ReadPositiveDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var s = Console.ReadLine();
                if (double.TryParse(s, out double val) && val > 0)
                    return val;
                Console.WriteLine("Помилка: введіть додатнє число. Спробуйте ще раз.\n");
            }
        }

        // Метод для демонстрації деструктора
        static void CreateAndDropTemp()
        {
            var temp = new Guitar("Tимчасова гітара", hasSolo: false);
            // після виходу зі scope об'єкт стає кандидатoм на збірку сміття
        }



    }
}