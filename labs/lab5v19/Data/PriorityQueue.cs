using System;
using System.Collections.Generic;

namespace lab5v19.Data
{
    // Проста черга з пріоритетом на базі SortedSet<T>
    // Вимагає IComparable<T>, щоб знати порядок
    public class PriorityQueue<T> where T : IComparable<T>
    {
        private readonly SortedSet<T> _set = new();

        public void Enqueue(T item) => _set.Add(item);

        public T Dequeue()
        {
            if (_set.Count == 0)
                throw new InvalidOperationException("Queue is empty");

            // Після перевірки Count компілятор все одно лякає "може бути null".
            // Для Min додаємо null-forgiving оператор '!' (значення точно не null).
            var first = _set.Min!;
            _set.Remove(first);
            return first;
        }

        public int Count => _set.Count;
        public IEnumerable<T> Items => _set;
    }
}
