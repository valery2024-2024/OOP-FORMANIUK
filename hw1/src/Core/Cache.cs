using System;
using System.Collections.Generic;

namespace hw1.Generics.Core
{
    // Узагальнений кеш з фіксованою місткістю. Обмеження: клас з публічним конструктором без параметрів
    public class Cache<T> where T : class, new()
    {
        // Приватне сховище — композиція Cache володіє списком і керує ним
        private readonly List<T> _items = new(); 
        // Публічна властивість лише для читання — максимальна місткість кешу
        public int Capacity { get; }

        // Конструктор ініціалізує місткість кешу.
        public Cache(int capacity)
        {
            if (capacity <= 0)
                throw new InvalidInputException("Місткість кешу має бути > 0.");
            Capacity = capacity;
        }

        /// додає елемент у кеш; якщо кеш переповнений  видаляє найстаріший елемент (FIFO).
        public void Add(T item)
        {
            // забороняю додавати null — це проста перевірка цілісності даних.
            if (item is null)
                throw new InvalidInputException("Неможливо додати null.");
            // Якщо кількість вже дорівнює місткості — вилучає перший доданий (найстаріший).
            if (_items.Count >= Capacity)
                RemoveOldest();
                // Додає новий елемент у кінець списку (він стане "найновішим").
            _items.Add(item);
        }

        // приватний метод видаляє перший елемен
        private void RemoveOldest()
        {
            if (_items.Count > 0)
            {
                _items.RemoveAt(0); 

            } 
        }

        // пошук першого елемента за умовою (без LINQ)
        public T Find(Predicate<T> match)
        {
            if (match is null) throw new InvalidInputException("Умова пошуку не може бути null.");
            // Ідемо послідовно й перевіряємо кожен елемент
            for (int i = 0; i < _items.Count; i++)
                if (match(_items[i]))
                    return _items[i];  // повертає першу відповідність
                    // Якщо сюди дійшли — елемент не знайдено
            throw new NotFoundException("Елемент не знайдено.");
        }

        // повертає новий список з елементами
        public List<T> Where(Predicate<T> match)
        {
            if (match is null) throw new InvalidInputException("Умова фільтрації не може бути null.");
            var result = new List<T>();
            // формує результат крок за кроком щоб не користуватися LINQ
            for (int i = 0; i < _items.Count; i++)
                if (match(_items[i]))
                    result.Add(_items[i]);
            return result;
        }

        // повертає копію всіх елементів
        public List<T> All()
        {
            return new List<T>(_items);
        }

        // видалення першого елемента  повертає true якщо видалення відбулося false  якщо нічого не знайдено
        public bool Remove(Predicate<T> match)
        {
            if (match is null)
                throw new InvalidInputException("Умова видалення не може бути null.");
            for (int i = 0; i < _items.Count; i++)
                if (match(_items[i]))
                {
                    _items.RemoveAt(i); // видаляє знайдений
                    return true; 
                }
            return false; // не знайшло — нічого не видалило
        }

        // сортування методом бульбашки за наданим IComparer<T> (без LINQ)
        public void BubbleSort(IComparer<T> comparer)
        {
            if (comparer is null) 
                throw new InvalidInputException("Порівнювач не може бути null.");

            // метод бульбашка
            for (int i = 0; i < _items.Count - 1; i++)
            {
                bool swapped = false;
                // внутрішній цикл порівнює сусідів і міняє місцями, якщо треба
                for (int j = 0; j < _items.Count - i - 1; j++)
                {
                    // comparer.Compare(a, b) > 0 => a > b (для порядку зростання)
                    if (comparer.Compare(_items[j], _items[j + 1]) > 0)
                    {
                        // кортежна перестановка (C#) — міняє місцями елементи
                        (_items[j], _items[j + 1]) = (_items[j + 1], _items[j]);
                        swapped = true;
                    }
                }
                if (!swapped) break; // оптимізація: масив уже відсортований
            }
        }

        // допоміжні обчислення над колекцією без LINQ (мін/макс за компаратором)
        public T Min(IComparer<T> comparer)
        {
            if (_items.Count == 0) throw new NotFoundException("Кеш порожній.");
            int idx = 0;// індекс поточного мінімуму
            for (int i = 1; i < _items.Count; i++)
                if (comparer.Compare(_items[i], _items[idx]) < 0)
                    idx = i; // знайшло менший — оновило індекс
            return _items[idx];
        }
        
        // пошук максимального елемента згідно компаратора comparer (без LINQ)
        public T Max(IComparer<T> comparer)
        {
            if (_items.Count == 0) throw new NotFoundException("Кеш порожній.");
            int idx = 0;// індекс поточного максимуму
            for (int i = 1; i < _items.Count; i++)
                if (comparer.Compare(_items[i], _items[idx]) > 0) idx = i;
            return _items[idx];
        }
    }
}
