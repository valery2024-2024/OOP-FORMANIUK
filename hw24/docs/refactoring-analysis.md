# Code Smells та рефакторинг: практичний аналіз

## Мета роботи

Ознайомитися з принципами рефакторингу, навчитися знаходити code smells у програмному коді та застосовувати техніки рефакторингу для покращення структури програми без зміни її функціональності.

---

## Теоретична частина

## Що таке рефакторинг

**Рефакторинг** — це процес покращення внутрішньої структури коду без зміни зовнішньої поведінки програми.

**Основні цілі рефакторингу:**

* покращення читабельності коду;
* зменшення складності;
* спрощення підтримки;
* полегшення тестування;
* спрощення додавання нових функцій.

Під час рефакторингу програма повинна працювати так само, як і до змін, але сам код стає більш чистим та зрозумілим.

## Практичний аналіз code smells у проєкті

**Для аналізу було використано власний C#-проєкт із використанням:**

* Repository Pattern;
* Strategy Pattern;
* Factory Pattern;
* Unit Of Work.

У проєкті було знайдено декілька різних code smells.

## 1. Switch Statements

### Де знайдено

**Файл:**

```text
ServiceRequestService.cs
```

### Проблема

У коді використовувався `switch` для вибору стратегії ціноутворення.

### Код до рефакторингу

```csharp
var strategy = pricingType switch
{
    "default" => new DefaultPricingStrategy(),
    "loyalty" => new LoyaltyPricingStrategy(),
    "urgency" => new UrgencyPricingStrategy(),
    _ => throw new ArgumentException("Unknown strategy")
};
```

### Чому це є проблемою

При додаванні нової стратегії потрібно змінювати вже існуючий код. Це порушує принцип Open/Closed Principle.

Також великі конструкції `switch` ускладнюють підтримку програми.

### Техніка рефакторингу

**Було використано:**

* Replace Conditional with Polymorphism;
* Factory Pattern.

### Код після рефакторингу

```csharp
public interface IPricingStrategy
{
    decimal CalculatePrice(decimal basePrice);
}
```

```csharp
public class DefaultPricingStrategy : IPricingStrategy
{
    public decimal CalculatePrice(decimal basePrice)
    {
        return basePrice;
    }
}
```

```csharp
public class LoyaltyPricingStrategy : IPricingStrategy
{
    public decimal CalculatePrice(decimal basePrice)
    {
        return basePrice * 0.9m;
    }
}
```

```csharp
public class UrgencyPricingStrategy : IPricingStrategy
{
    public decimal CalculatePrice(decimal basePrice)
    {
        return basePrice * 1.2m;
    }
}
```

### Результат

Код став більш гнучким. Для додавання нової стратегії тепер достатньо створити новий клас без зміни старого коду.

## 2. Long Parameter List

### Де знайдено/

**Файл:**

```text
VehicleService.cs
```

### Проблема/

Метод створення автомобіля приймав велику кількість параметрів.

### Код до рефакторингу/

```csharp
CreateVehicle(string brand,
              string model,
              int year,
              string vin,
              int clientId)
```

### Чому це є проблемою/

* важко читати метод;
* легко переплутати параметри;
* складніше викликати метод;
* погіршується підтримка коду.

### Техніка рефакторингу/

**Було використано:**

* Introduce Parameter Object.

### Код після рефакторингу/

```csharp
public class CreateVehicleDto
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string Vin { get; set; }
    public int ClientId { get; set; }
}
```

```csharp
CreateVehicle(CreateVehicleDto dto)
```

### Результат/

Метод став простішим та зрозумілішим. Дані тепер згруповані в одному об’єкті.

## 3. Primitive Obsession

### Де знайдено//

**Файл:**

```text
Vehicle.cs
```

### Проблема//

VIN-код автомобіля зберігався як звичайний рядок.

### Код до рефакторингу//

```csharp
public string Vin { get; set; }
```

### Чому це є проблемою//

**VIN-код має:**

* фіксовану довжину;
* спеціальний формат;
* потребує перевірки.

Але звичайний `string` не забезпечує валідацію.

### Техніка рефакторингу//

**Було використано:**

* Replace Data Value with Object.

### Код після рефакторингу//

```csharp
public class VehicleVin
{
    public string Value { get; }

    public VehicleVin(string value)
    {
        if (value.Length != 17)
        {
            throw new ArgumentException("Invalid VIN");
        }

        Value = value;
    }
}
```

### Результат//

Тепер VIN-код проходить перевірку при створенні об’єкта, що зменшує кількість можливих помилок.

## 4. Duplicate Code

### Де знайдено///

**Файли:**

* ClientRepository.cs
* VehicleRepository.cs
* ServiceRequestRepository.cs

### Проблема///

У різних repository повторювались однакові CRUD-операції.

### Код до рефакторингу///

```csharp
AddClient()
AddVehicle()
AddServiceRequest()
```

### Чому це є проблемою///

* дублювання коду;
* складніше вносити зміни;
* більше шансів на помилки.

### Техніка рефакторингу///

**Було використано:**

* Extract Class;
* Generic Repository.

### Код після рефакторингу///

```csharp
public class Repository<T> : IRepository<T>
{
    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }
}
```

### Результат///

Повторення коду було зменшено. Один універсальний repository тепер використовується для різних сутностей.

## 5. Large Class (God Object)

### Де знайдено////

**Файл:**

```text
Program.cs
```

### Проблема////

У `Program.cs` знаходилась велика кількість логіки:

* меню;
* введення даних;
* створення об’єктів;
* виклики сервісів;
* виведення інформації.

### Чому це є проблемою////

**Такий клас:**

* важко підтримувати;
* важко тестувати;
* важко розширювати.

### Техніка рефакторингу////

**Було використано:**

* Extract Class;
* розділення відповідальностей.

### Приклад покращення

**Було створено окремі сервіси:**

* MenuService;
* InputService;
* VehicleService;
* ClientService.

### Результат////

Код став більш структурованим та зрозумілим.

## Чому рефакторинг без тестів є ризикованим

Рефакторинг без тестів є небезпечним, тому що під час зміни структури коду можна випадково змінити логіку роботи програми.

**Наприклад:**

**Було:**

```csharp
return basePrice * 0.9m;
```

**Після рефакторингу помилково стало:**

```csharp
return basePrice * 9m;
```

Програма запуститься, але результат буде неправильним.

Саме тому перед рефакторингом бажано мати автоматичні тести, які перевіряють правильність роботи програми.

## Висновок

**У процесі виконання роботи було проаналізовано власний C#-проєкт та знайдено декілька code smells:**

* Switch Statements;
* Long Parameter List;
* Primitive Obsession;
* Duplicate Code;
* Large Class.

**Для усунення проблем було застосовано різні техніки рефакторингу:**

* Factory Pattern;
* Strategy Pattern;
* Generic Repository;
* Introduce Parameter Object;
* Replace Data Value with Object.

**Після рефакторингу код став:**

* більш чистим;
* зрозумілішим;
* гнучкішим;
* легшим для підтримки та розширення.

Рефакторинг дозволяє підтримувати якість програмного забезпечення та спрощує подальшу розробку проєкту.
