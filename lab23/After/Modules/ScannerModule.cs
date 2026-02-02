using System;
using lab23.After.Interfaces;

namespace lab23.After.Modules;

public class ScannerModule : IScanner
{
    public void Scan(string document)
    {
        Console.WriteLine($"[After][ScannerModule] Сканування: {document}");
    }
}
