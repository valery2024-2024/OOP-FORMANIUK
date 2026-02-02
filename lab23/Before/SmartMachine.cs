namespace lab23.Before;

// порушення DIP клас напряму залежить від конкретних модулів
// порушення ISP змушений мати всі методи, навіть якщо треба лише один
public class SmartMachine : IMultifunctionDevice
{
    // жорсткий зв'язок створення залежностей прямо в класі
    private readonly PrinterModule _printer = new PrinterModule();
    private readonly ScannerModule _scanner = new ScannerModule();
    private readonly FaxModule _fax = new FaxModule();

    public void Print(string document) => _printer.Print(document);

    public void Scan(string document) => _scanner.Scan(document);

    public void Fax(string document) => _fax.Fax(document);
}
