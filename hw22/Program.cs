using System;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

public class Student
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public int Age { get; set; }
    public string Group { get; set; }
    public double AverageGrade { get; set; }
}

class Program
{
    static void Main()
    {
        Student student = new Student
        {
            Id = 1,
            FullName = "Валерій Форманюк",
            Age = 44,
            Group = "КН-3/1",
            AverageGrade = 4
        };

        var jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(student, jsonOptions);
        File.WriteAllText("student.json", json);

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Student));

        using (StreamWriter writer = new StreamWriter("student.xml"))
        {
            xmlSerializer.Serialize(writer, student);
        }

        Console.WriteLine("JSON:");
        Console.WriteLine(json);

        Console.WriteLine();

        Console.WriteLine("XML файл створено: student.xml");
    }
}