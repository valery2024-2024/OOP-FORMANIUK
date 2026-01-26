# **Домашня робота №3**

## **Принципи `ISP` та `DIP`**

### **Мета роботи**

Ознайомитись з принципами `ISP` та `DIP`, розглянути приклад порушення `ISP`, запропонувати рішення та показати застосування `DIP` через `Dependecy Injection`.

### **1. Принцип `IPS (Interface Segregation Principle)`**

ISP означає, що краще мати кілька маленьких інтерфейсів, ніж один великий. Клас не повинен залежати від методів, які він не використовує.

#### **Приклад порушення ISP**

```csharp
public interface IWorker
{
    void Work();
    void Eat();
    void Sleep();
}


public class Robot : IWorker
{
    public void Work()
    {
        Console.WriteLine("Robot is working");
    }

    public void Eat()
    {
        // Роботу не потрібно їсти
        throw new NotImplementedException();
    }

    public void Sleep()
    {
        // Роботу не потрібно спати
        throw new NotImplementedException();
    }
}
```

Проблема: робот змушений реалізовувати `Eat()` i `Sleep()`, які йому не потрібні. Це і є порушення ISP.

#### **Виправлення ISP**

```csharp
public interface IWorkable
{
    void Work();
}

public interface IEatable
{
    void Eat();
}

public interface ISleepable
{
    void Sleep();
}


public class Robot : IWorkable
{
    public void Work()
    {
        Console.WriteLine("Robot is working");
    }
}
```

Тепер кожен клас реалізує тільки потрібний йому інтерфейс.

### **2. Принцип `DIP (Dependency Inversion Principle)`**

DIP означає що високорівневі модулі не повинні залежати від конкретних класів і ті, і ті повинні залежати від абстракцій(інтерфейсів)

#### **Застосування `DIP` через `Dependency Injection`**

```csharp
public interface IMessageService
{
    void Send(string message);
}


public class EmailService : IMessageService
{
    public void Send(string message)
    {
        Console.WriteLine("Email: " + message);
    }
}


public class Notification
{
    private readonly IMessageService _messageService;

    public Notification(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public void Notify(string text)
    {
        _messageService.Send(text);
    }
}
```

Клас `Notification` не залежить від конкретного `EmailService`, а працює через інтерфейс. Це і є `Dependency Injection` та дотримання `DIP`.

### **3. Як `ISP` покращує  `DIP` і тестування.**

Маленькі інтерфейси легше підміняти в тестах.
Класи стають менш залежними один від одного.
Код простіше підтримувати та розширювати.
Легше писати unit-тести з моками.

### **Висновок**

Принципи `ISP` та `DIP` допомагають будувати чисту та гнучку архітектуру. Вони зменшують залежності між класами, полегшують тестування та подальший розвиток програми. Дотримання цих принципів підвищує якість програмного коду.
