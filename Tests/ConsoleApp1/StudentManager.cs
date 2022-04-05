using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp1;

internal class StudentManager
{
    private int _LastFreeId = 1;
    private List<Student> students = new();
    public Student Add (string LastName, string FirstName, string Patronymic, DateTime Birthday)
    {
        var student = new Student
        {
            Id = _LastFreeId++,
            LastName = LastName,
            FirstName = FirstName,
            Patronymic = Patronymic,
            Birtday = Birthday
        };
        students.Add(student);
        return student;
    }

    public FileInfo SaveTo(string filePath)
    {
        var file = new FileInfo(filePath);
        var xml_serializer = new XmlSerializer(typeof(List<Student>));

        using(var xml = file.CreateText())
            xml_serializer.Serialize(xml, students);
        return file;
    }
}