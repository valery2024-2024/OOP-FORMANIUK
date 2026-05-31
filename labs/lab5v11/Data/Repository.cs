using System;
using System.Collections.Generic;
using System.Linq;

namespace lab5v11.Data
{
    public interface IRepository<T>
    {
        // Інтерфейс для базових CRUD-операцій
        void Add(T item);  // Додати елемент
        bool Remove(Predicate<T> predicate);  // Видалити елемент за умовою
        T Find(Func<T, bool> predicate);       // Знайти елемент (якщо немає — виняток)
        IReadOnlyList<T> All();  // Повернути всі елементи

        IEnumerable<T> Where(Func<T, bool> predicate); // LINQ-пошук за умовою
    }

    // Узагальнений клас Repository<T> для роботи з будь-яким типом
    public class Repository<T> : IRepository<T>
    {
        // Внутрішня колекція для збереження елементів
        private readonly List<T> _items = new();

        // Додати елемент у репозиторій
        public void Add(T item) => _items.Add(item);

        // Видалити перший елемент, який задовольняє умову
        public bool Remove(Predicate<T> predicate)
        {
            var idx = _items.FindIndex(predicate);
            if (idx >= 0) { _items.RemoveAt(idx); return true; }
            return false;
        }

        // Знайти елемент за умовою або кинути виняток, якщо не знайдено
        public T Find(Func<T, bool> predicate)
            => _items.FirstOrDefault(predicate) ?? throw new Exceptions.NotFoundException(typeof(T).Name + " not found");

        // Повернути всі елементи як колекцію лише для читання
        public IReadOnlyList<T> All() => _items.AsReadOnly();

        // Повернути колекцію елементів, що задовольняють умову
        public IEnumerable<T> Where(Func<T, bool> predicate) => _items.Where(predicate);
    }
}
