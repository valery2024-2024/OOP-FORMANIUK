namespace lab23.Before;

// порушення ISP один інтерфейс змушує реалізовувати все одразу (print/scan/fax)
public interface IMultifunctionDevice
{
    void Print(string document);
    void Scan(string document);
    void Fax(string document);
}
