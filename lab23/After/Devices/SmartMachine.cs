using lab23.After.Interfaces;

namespace lab23.After.Devices;

// DIP залежить від інтерфейсів, а не від конкретних класів
// DI залежності отримує через конструктор
public class SmartMachine
{
    private readonly IPrinter _printer;
    private readonly IScanner _scanner;
    private readonly IFax _fax;

    // Dependency Injection через конструктор
    public SmartMachine(IPrinter printer, IScanner scanner, IFax fax)
    {
        _printer = printer;
        _scanner = scanner;
        _fax = fax;
    }

    public void Print(string document) => _printer.Print(document);

    public void Scan(string document) => _scanner.Scan(document);

    public void Fax(string document) => _fax.Fax(document);
}
