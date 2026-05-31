using System;
using System.Collections.Generic;
using System.Net.Http;

public class NetworkClient
{
    private int _failCount = 0;

    public List<string> GetProductsFromApi(string url)
    {
        // Імітує HttpRequestException перші 3 рази
        if (_failCount < 3)
        {
            _failCount++;
            Console.WriteLine($"[NetworkClient] Помилка №{_failCount}: API недоступне");
            throw new HttpRequestException("API тимчасово недоступне");
        }

        // Успіх
        Console.WriteLine("[NetworkClient] Дані успішно отримані з API!");
        return new List<string> { "Cheese", "Juice", "Tomatoes" };
    }
}
