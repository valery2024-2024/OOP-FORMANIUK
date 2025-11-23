using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using Polly.CircuitBreaker;
using Polly.Timeout;
using Polly.Fallback;
using Polly.Retry;

public class Program
{
    private static int _apiAttempts = 0;
    private static int _dbAttempts = 0;
    private static int _queueAttempts = 0;

    public static async Task Main()
    {
        Console.WriteLine("IndependentWork11 (Polly demo)\n");

        await Scenario1_Retry();
        await Scenario2_TimeoutRetry();
        await Scenario3_CircuitBreakerFallback();

        Console.WriteLine("End");
    }

    // СЦЕНАРІЙ 1 — Retry (API тимчасово падає)
    private static async Task Scenario1_Retry()
    {
        Console.WriteLine("Scenario 1: Retry");

        var retry = Policy<string>
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(
                3,
                attempt => TimeSpan.FromSeconds(1 * attempt),
                (outcome, ts, retryCount, ctx) =>
                {
                    Console.WriteLine($"Retry {retryCount}: {outcome.Exception?.Message}");
                });

        try
        {
            var result = await retry.ExecuteAsync(() => FakeApiCall());
            Console.WriteLine($"API OK: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API FAILED: {ex.Message}");
        }

        Console.WriteLine("End Scenario 1\n");
    }

    private static Task<string> FakeApiCall()
    {
        _apiAttempts++;
        Console.WriteLine($"API attempt {_apiAttempts}");

        if (_apiAttempts <= 2)
            throw new HttpRequestException("API temporary unavailable");

        return Task.FromResult("DATA_OK");
    }

    // СЦЕНАРІЙ 2 — Timeout + Retry
    private static async Task Scenario2_TimeoutRetry()
    {
        Console.WriteLine("Scenario 2: Timeout + Retry");

        var timeout = Policy.TimeoutAsync(2, TimeoutStrategy.Pessimistic);

        var retry = Policy
            .Handle<TimeoutRejectedException>()
            .WaitAndRetryAsync(2, _ => TimeSpan.FromSeconds(1));

        var combined = retry.WrapAsync(timeout);

        try
        {
            await combined.ExecuteAsync(async () =>
            {
                _dbAttempts++;
                Console.WriteLine($"DB attempt {_dbAttempts}");

                if (_dbAttempts == 1)
                    await Task.Delay(3000); // викличе timeout
                else
                    await Task.Delay(500); // успіх

            });

            Console.WriteLine("DB query OK");
        }
        catch
        {
            Console.WriteLine("DB FAILED");
        }

        Console.WriteLine("End Scenario 2\n");
    }

    // СЦЕНАРІЙ 3 — CircuitBreaker + Fallback (версія для Polly v8)
    private static async Task Scenario3_CircuitBreakerFallback()
    {
        Console.WriteLine("Scenario 3: CircuitBreaker + Fallback");

        // Створюємо PolicyBuilder
        var builder = Policy<bool>
            .Handle<Exception>();

        // CircuitBreaker у Polly v8 створюється так:
        var breaker = builder.CircuitBreakerAsync(
            handledEventsAllowedBeforeBreaking: 2,
            durationOfBreak: TimeSpan.FromSeconds(5),
            onBreak: (outcome, breakDelay) =>
            {
                Console.WriteLine($"CIRCUIT OPEN: {outcome.Exception?.Message}");
            },
            onReset: () =>
            {
                Console.WriteLine("CIRCUIT RESET");
            },
            onHalfOpen: () =>
            {
                Console.WriteLine("CIRCUIT HALF-OPEN");
            }
        );

        // Fallback
        var fallback = Policy<bool>
            .Handle<BrokenCircuitException>()
            .Or<Exception>()
            .FallbackAsync(
                fallbackValue: false,
                onFallbackAsync: (outcome, ctx) =>
                {
                    Console.WriteLine($"Fallback triggered: {outcome.Exception?.GetType().Name}");
                    return Task.CompletedTask;
                }
            );

        // Комбінує
        var policy = fallback.WrapAsync(breaker);

       for (int i = 1; i <= 5; i++)
       {
           bool sent = await policy.ExecuteAsync(async () =>
           {
               await SendToQueue($"hello #{i}");
               return true;
           });

           if (sent)
               Console.WriteLine($"Message {i}: SENT\n");
           else
               Console.WriteLine($"Message {i}: SAVED LOCALLY (fallback)\n");

           await Task.Delay(700);
        }

        Console.WriteLine("End Scenario 3\n");
    }

    private static Task SendToQueue(string msg)
    {
        _queueAttempts++;

        Console.WriteLine($"Queue attempt {_queueAttempts}: sending '{msg}'");

        if (_queueAttempts <= 3)
            throw new Exception("Queue overloaded");

        return Task.CompletedTask;
    }
}
