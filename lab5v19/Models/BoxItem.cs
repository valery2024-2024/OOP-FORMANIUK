using System;

namespace lab5v19.Models
{
    // Реалізуємо IComparable<BoxItem>, щоб працювали SortedSet/PriorityQueue
    public class BoxItem : IComparable<BoxItem>
    {
        public string Name { get; }       // Назва
        public double Volume { get; }     // Об’єм (умовна одиниця)

        public BoxItem(string name, double volume)
        {
            Name = name;
            Volume = volume;
        }

        // Порівнюємо спочатку за об’ємом (менший — вищий пріоритет),
        // а якщо об’єми рівні — за назвою (щоб уникати "дубля" у SortedSet)
        public int CompareTo(BoxItem? other)
        {
            if (other is null) return 1;
            int byVolume = Volume.CompareTo(other.Volume);
            if (byVolume != 0) return byVolume;
            return string.Compare(Name, other.Name, StringComparison.Ordinal);
        }

        public override string ToString() => $"{Name} ({Volume:F1} л)";
    }
}
