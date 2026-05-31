public class BadReportGenerator
{
    public string GetReportDataFromDatabase() { /* ... */ return ""; }

    public string ProcessData(string rawData) { /* ... */ return ""; }

    public string FormatHtmlReport(string processedData) { /* ... */ return ""; }

    public void SaveToFile(string fileName, string content) { /* ... */ }

    public void GenerateAndSaveReport(string fileName)
    {
        string rawData = GetReportDataFromDatabase();
        string processedData = ProcessData(rawData);
        string htmlReport = FormatHtmlReport(processedData);
        SaveToFile(fileName, htmlReport);
    }
}
