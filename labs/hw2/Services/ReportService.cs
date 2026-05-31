public class ReportService
{
    private readonly IReportDataSource _dataSource;
    private readonly IDataProcessor _dataProcessor;
    private readonly IReportFormatter _formatter;
    private readonly IReportSaver _saver;

    public ReportService(
        IReportDataSource dataSource,
        IDataProcessor dataProcessor,
        IReportFormatter formatter,
        IReportSaver saver)
    {
        _dataSource = dataSource;
        _dataProcessor = dataProcessor;
        _formatter = formatter;
        _saver = saver;
    }

    public void GenerateAndSaveReport(string fileName)
    {
        string rawData = _dataSource.GetRawData();
        string processedData = _dataProcessor.Process(rawData);
        string formattedReport = _formatter.Format(processedData);
        _saver.Save(fileName, formattedReport);
    }
}
