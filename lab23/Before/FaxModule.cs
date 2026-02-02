using System;

namespace lab23.Before;

public class FaxModule
{
    public void Fax(string document)
    {
        Console.WriteLine($"[Before][FaxModule] Факс: {document}");
    }
}
