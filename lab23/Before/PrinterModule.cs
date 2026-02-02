using System;

namespace lab23.Before;

public class PrinterModule
{
    public void Print(string document)
    {
        Console.WriteLine($"[Before][PrinterModule] Друк: {document}");
    }
}
