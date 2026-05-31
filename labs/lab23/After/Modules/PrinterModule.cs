using System;
using lab23.After.Interfaces;

namespace lab23.After.Modules;

// реалізація інтерфейсу IPrinter
public class PrinterModule : IPrinter
{
    public void Print(string document)
    {
        Console.WriteLine($"[After][PrinterModule] Друк: {document}");
    }
}
