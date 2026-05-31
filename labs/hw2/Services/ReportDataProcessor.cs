public class ReportDataProcessor : IDataProcessor
{
    public string Process(string rawData) => rawData.ToUpper();
}
