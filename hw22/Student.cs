using System;
using System.Text.Json;
using System.Xml.Serialization;
using System.IO;

public class Student
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public int Age { get; set; }
    public string Group { get; set; }
    public double AverageGrade { get; set; }
}