using ConsoleApp1;
using System.Xml.Serialization;


var student_manager = new StudentManager();

student_manager.Add("Иванов", "Иван", "Иванович", DateTime.Now.AddYears(-18));
student_manager.Add("Петров", "Петр", "Петрович", DateTime.Now.AddYears(-36));
student_manager.Add("Дмитриев", "Дмитрий", "Дмитриевич", DateTime.Now.AddYears(-25));

const string file_name = "students.xml";
student_manager.SaveTo(file_name);
Console.ReadLine();
