using System;
public class CloudReportSaver : IReportSaver
{
    public void Save(string fileName, string content)
    {
        Console.WriteLine("Saving report to cloud");
    }
}
