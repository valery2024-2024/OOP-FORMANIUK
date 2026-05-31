# Самостійна робота №15

## Аналіз SRP та OCP у відкритому Open - Source проєкті

**Тема** Аналіз SRP/OCP у відкритих репозиторіях
**Мета** Дослідити застосування принципів SRP та OCP у реальному Open - Source проєкті на C# і оцінити їхній вплив на архітектуру та якість коду.

## 1. Обраний проєкт

- **Назва:** [MediatR]
- **Посилання на GitHub:** [https://github.com/LuckyPennySoftware/MediatR];

- **Короткий опис:** `MediatR` це Open - Source бібліотека для .NET, яка реалізує патерн `Mediator`. Вона дозволяє розділяти запити, команди та їх обробники, зменшуючи залежності між компонентами та покращуючи архітектуру застосунків.

## 2. Аналіз SRP (Single Responsibility Principle)

### 2.1 Приклади дотримання SRP

#### Клас: [`IMediator`]

- **Відповідальність:** Визначає єдиний контракт mediator-a для надсилання запитів та публікації повідомлень.

- **Обґрунтування:** Інтерфейс `IMediator` не містить бізнес-логіки та реалізації. Його єдина роль описати контракт взаємодії між відправником і обробниками, що відповідає SRP.

```csharp
namespace MediatR;

/// <summary>
/// Defines a mediator to encapsulate request/response and publishing interaction patterns
/// </summary>
public interface IMediator : ISender, IPublisher
{
}
```

#### Клас: [`IRequestHandler<TRequest, TResponse>`]

- **Відповідальність:** Описує контракт для обробки одного конкретного запиту та повернення відповіді.

- **Обґрунтування:** Інтерфейс відповідає лише за метод обробки запиту `Handle`, не виконуючи додаткових задач.

```csharp
public interface IRequestHandler<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
```

#### Клас: [`ISender`]

- **Відповідальність:** Визначає контракт для надсилання запитів через `mediator pipeline`.

- **Обґрунтування:** `ISender` відповідає лише за операції `Send` і не містить логіки обробки чи створення `handler-ів.`

```csharp
public interface ISender
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
    Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest;
}
```

### Приклад порушення SRP

#### Клас: [`Mediator`]

- **Множинні відповідальності**

    - Надсилання та публікація запитів
    - Робота з `Dependency Injection`
    - Кешування обгорток обробників
    - Аналіз типів через reflection
    - Ліцензійна логіка

- **Проблеми** Клас має багато причин для змін, що ускладнює тестування та 

```csharp
var handler = (RequestHandlerWrapper)_requestHandlers.GetOrAdd(request.GetType(), static requestType =>
        {
            var wrapperType = typeof(RequestHandlerWrapperImpl<>).MakeGenericType(requestType);
            var wrapper = Activator.CreateInstance(wrapperType) ?? throw new InvalidOperationException($"Could not create wrapper type for {requestType}");
            return (RequestHandlerBase)wrapper;
        });
```

## 3. Аналіз OCP (Open/Closed Principle)

### 3.1 Приклади дотримання OCP

#### Сценарій/Модуль: [`Pipeline Behaviors`]

- **Механізм розширення:** Інтерфейс [`IPipelineBehavior<TRecuest, TResponce>`]
- **Обґрунтування:** Нові поведінки додаються через реалізацію інтерфейсу без зміни ядра бібліотеки.

```csharp
public interface IPipelineBehavior<in TRequest, TResponse> where TRequest : notnull
{
    /// <summary>
    /// Pipeline handler. Perform any additional behavior and await the <paramref name="next"/> delegate as necessary
    /// </summary>
    /// <param name="request">Incoming request</param>
    /// <param name="next">Awaitable delegate for the next action in the pipeline. Eventually this delegate represents the handler.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Awaitable task returning the <typeparamref name="TResponse"/></returns>
    Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken);
}
```

#### Сценарій/Модуль: [`Notification Publishing`]

- **Механізм розширення:** Інтерфейс [`INotificationPublisher`]
- **Обґрунтування:** Різні сценарії публікації повідомлень реалізуються без зміни класу Mediator.

```csharp
public interface INotificationPublisher
{
    Task Publish(IEnumerable<NotificationHandlerExecutor> handlerExecutors, INotification notification,
        CancellationToken cancellationToken);
}
```

### 3.2 Приклад порушення OCP

#### Сценарій/Модуль: Публікація повідомлень у [`Mediator`]

- **Проблема:** Метод [`Publish(object notification, ...)`] використовує оператор [`switch`] для визначення типу повідомлення. Додавання нових типів або способів обробки потребуватиме зміни існуючого коду.
- **Наслідки:**

    - Порушення `Open/Closed Principle`
    - Підвищений ризик помилок при розширенні функціональності
    - Зменшення гнучкості системи

```csharp
public Task Publish(object notification, CancellationToken cancellationToken = default) =>
        notification switch
        {
            null => throw new ArgumentNullException(nameof(notification)),
            INotification instance => PublishNotification(instance, cancellationToken),
            _ => throw new ArgumentException($"{nameof(notification)} does not implement ${nameof(INotification)}")
        };
```

## 4. Загальні висновки

Під час аналізу проєкту `Mediator` було виявлено грамотне застосування принципів `SRP` та `OCP`. Інтерфейси мають чіткі зони відповідальності, а механізми розширення дозволяють додавати нову функціональність без зміни існуючого коду. Водночас деякі класи поєднують декілька відповідальностей, що може ускладнювати підтримку. Загалом архітектура проєкту є якісною та відповідає принципам `SOLID`.
