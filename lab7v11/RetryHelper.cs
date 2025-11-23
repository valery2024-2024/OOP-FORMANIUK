using System;
using System.Threading;

public static class RetryHelper
{
    public static T ExecuteWithRetry<T>(
        Func<T> operation,
        int retryCount = 3,
        TimeSpan initialDelay = default,
        Func<Exception, bool> shouldRetry = null)
    {
        if (initialDelay == default)
            initialDelay = TimeSpan.FromMilliseconds(500);

        int attempt = 0;

        while (true)
        {
            try
            {
                attempt++;
                Console.WriteLine($"Спроба №{attempt}");
                return operation();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");

                if (attempt >= retryCount || (shouldRetry != null && !shouldRetry(ex)))
                {
                    Console.WriteLine("Повторна спроба НЕ буде виконана.");
                    throw;
                }

                // Експоненційна затримка: delay * 2^attempt
                var delay = TimeSpan.FromMilliseconds(initialDelay.TotalMilliseconds * Math.Pow(2, attempt - 1));
                Console.WriteLine($"Очікування {delay.TotalMilliseconds} мс перед повтором...");
                Thread.Sleep(delay);
            }
        }
    }
}
