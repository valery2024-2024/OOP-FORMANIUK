using System;
public class FileReportSaver : IReportSaver
{
    public void Save(string fileName, string content)
    {
        Console.WriteLine("Saving report to file");
    }
}
