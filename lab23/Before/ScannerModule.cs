using System;

namespace lab23.Before;

public class ScannerModule
{
    public void Scan(string document)
    {
        Console.WriteLine($"[Before][ScannerModule] Сканування: {document}");
    }
}
