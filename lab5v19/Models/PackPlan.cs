using System;
using System.Collections.Generic;
using System.Linq;
using lab5v19.Exceptions;

namespace lab5v19.Models
{
    // Клас для пакування предметів у бокси (композиція)
    public class PackPlan
    {
        private readonly List<BoxItem> _boxes = new();  // Контейнер для боксів
        private readonly Dictionary<BoxItem, double> _used = new(); // Зайнятий обсяг кожного боксу

        // Додаємо новий бокс
        public void AddBox(BoxItem box)
        {
            _boxes.Add(box);
            _used[box] = 0;  // Початково порожній
        }

        // Спроба розмістити предмет (жадібний алгоритм)
        public void PlaceItem(BoxItem item)
        {
            // Знаходимо перший бокс, куди предмет влізе
            var box = _boxes.FirstOrDefault(b => _used[b] + item.Volume <= b.Volume);

            if (box == null)
                throw new ItemTooLargeException($"Предмет {item.Name} ({item.Volume} л) не вміщується у жоден бокс!");

            // Додаємо об’єм предмету до використаного простору боксу
            _used[box] += item.Volume;
        }

        // Обчислити заповненість (%) кожного боксу
        public Dictionary<BoxItem, double> GetFillRates()
        {
            return _boxes.ToDictionary(b => b, b => Math.Round(_used[b] / b.Volume * 100, 1));
        }

        // Вивести інформацію про стан усіх боксів
        public void PrintStatus()
        {
            Console.WriteLine("Стан боксів:");
            foreach (var b in _boxes)
            {
                Console.WriteLine($"- {b.Name,-10} заповнено: {_used[b],5:F1}/{b.Volume:F1} л ({_used[b] / b.Volume * 100:F1}%)");
            }
        }
    }
}
