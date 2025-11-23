using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var fileProcessor = new FileProcessor();
        var networkClient = new NetworkClient();

        // shouldRetry: повторює лише якщо FileNotFoundException або HttpRequestException
        bool ShouldRetry(Exception ex) =>
            ex is System.IO.FileNotFoundException || ex is HttpRequestException;

        Console.WriteLine("Отримання продуктів з файлу");
        var fileProducts = RetryHelper.ExecuteWithRetry(
            () => fileProcessor.LoadProductNames("products.txt"),
            retryCount: 4,
            initialDelay: TimeSpan.FromMilliseconds(300),
            shouldRetry: ShouldRetry
        );

        Console.WriteLine("\nОтримання продуктів з API");
        var apiProducts = RetryHelper.ExecuteWithRetry(
            () => networkClient.GetProductsFromApi("https://api.fake.com"),
            retryCount: 5,
            initialDelay: TimeSpan.FromMilliseconds(300),
            shouldRetry: ShouldRetry
        );

        Console.WriteLine("\nРезультати");
        Console.WriteLine("Продукти з файлу: " + string.Join(", ", fileProducts));
        Console.WriteLine("Продукти з API: " + string.Join(", ", apiProducts));
    }
}
