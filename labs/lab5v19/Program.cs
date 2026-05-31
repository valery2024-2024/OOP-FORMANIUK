using System;
using lab5v19.Data;
using lab5v19.Models;
using lab5v19.Exceptions;

namespace lab5v19
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Лабораторна №5 — Generics + PriorityQueue + Exceptions (варіант 19: Пакування на складі)\n");

            // Створюємо бокс-план (PackPlan)
            var plan = new PackPlan();
            plan.AddBox(new BoxItem("Box A", 10));
            plan.AddBox(new BoxItem("Box B", 15));
            plan.AddBox(new BoxItem("Box C", 8));

            // Створюємо чергу предметів (PriorityQueue<>)
            var queue = new PriorityQueue<BoxItem>();

            // Менший об’єм вищий пріоритет (жадібний алгоритм)
            queue.Enqueue(new BoxItem("Книга", 3));
            queue.Enqueue(new BoxItem("Ноутбук", 5));
            queue.Enqueue(new BoxItem("Сумка для ноутбука", 7));
            queue.Enqueue(new BoxItem("Мікрохвильовка", 12)); // завеликий предмет
            queue.Enqueue(new BoxItem("Мишка", 1));

            // Розміщуємо предмети по боксах
            while (queue.Count > 0)
            {
                var item = queue.Dequeue();
                try
                {
                    plan.PlaceItem(item);
                    Console.WriteLine($"Розміщено {item.Name} ({item.Volume} л)");
                }
                catch (ItemTooLargeException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }

            Console.WriteLine();
            plan.PrintStatus();

            // Виводимо відсотки заповненості
            Console.WriteLine("\nВідсоток заповненості боксів:");
            foreach (var kvp in plan.GetFillRates())
            {
                Console.WriteLine($"{kvp.Key.Name,-10} — {kvp.Value}%");
            }

            Console.WriteLine("\nГотово");
        }
    }
}
