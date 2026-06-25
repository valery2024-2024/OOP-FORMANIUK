using System;
using System.Threading;

public static class RetryHelper
{
    public static void ExecuteWithRetry(
        Action action,
        int maxRetries = 3)
    {
        int attempts = 0;

        while (true)
        {
            try
            {
                action();
                return;
            }
            catch
            {
                attempts++;

                if (attempts >= maxRetries)
                    throw;

                Thread.Sleep(500);
            }
        }
    }
}