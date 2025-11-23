using System;
using System.Collections.Generic;
using System.IO;

public class FileProcessor
{
    private int _failCount = 0;

    public List<string> LoadProductNames(string path)
    {
        // Імітує FileNotFoundException перші 2 рази
        if (_failCount < 2)
        {
            _failCount++;
            Console.WriteLine($"[FileProcessor] Помилка №{_failCount}: файл не знайдено");
            throw new FileNotFoundException("Файл недоступний");
        }

        // Успішне виконання
        Console.WriteLine("[FileProcessor] Файл успішно прочитаний!");
        return new List<string> { "Bread", "Milk", "Eggs" };
    }
}
