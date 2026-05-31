using System;
using lab23.After.Interfaces;

namespace lab23.After.Modules;

public class FaxModule : IFax
{
    public void Fax(string document)
    {
        Console.WriteLine($"[After][FaxModule] Факс: {document}");
    }
}
