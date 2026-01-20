public class HtmlReportFormatter : IReportFormatter
{
    public string Format(string processedData) => $"<html><body>{processedData}</body></html>";
}
