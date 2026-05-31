using System;
var reportService = new ReportService(
    new DatabaseDataSource(),
    new ReportDataProcessor(),
    new HtmlReportFormatter(),
    new FileReportSaver()
);

reportService.GenerateAndSaveReport("report.html");

Console.WriteLine("Done");
