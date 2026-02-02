using System;

// BEFORE
using lab23.Before;

// AFTER
using lab23.After.Devices;
using lab23.After.Interfaces;
using lab23.After.Modules;

namespace lab23;

class Program
{
    static void Main()
    {
        Console.WriteLine("BEFORE (порушує ISP та DIP)");

        // тут SmartMachine сам створює залежності всередині себе
        IMultifunctionDevice badDevice = new lab23.Before.SmartMachine();

        badDevice.Print("Doc_A.pdf");
        badDevice.Scan("Doc_A.pdf");
        badDevice.Fax("Doc_A.pdf");

        Console.WriteLine("\nAFTER (ISP + DIP + DI через конструктор)");

        // в Main конфігурує залежність
        IPrinter printer = new lab23.After.Modules.PrinterModule();
        IScanner scanner = new lab23.After.Modules.ScannerModule();
        IFax fax = new lab23.After.Modules.FaxModule();

        // впровадження залежності через конструктор (DI)
        var goodDevice = new lab23.After.Devices.SmartMachine(printer, scanner, fax);

        goodDevice.Print("Doc_B.pdf");
        goodDevice.Scan("Doc_B.pdf");
        goodDevice.Fax("Doc_B.pdf");

        Console.WriteLine("\nГотово");
    }
}
