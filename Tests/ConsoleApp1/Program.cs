using ConsoleApp1;
using System.Xml.Serialization;


var student = new Student
{
    Id = 1,
    FirstName = "Ivan",
    LastName = "Ivanov",
    Patronymic = "Ivanovich",
    Birtday = DateTime.Now.AddYears(-18)
    
};

var xml_sirialiser = new XmlSerializer(typeof(Student));

const string xml_file_name = "student.xml";

using (var xml_file = File.CreateText(xml_file_name))
{
    xml_sirialiser.Serialize(xml_file, student);

};
// using тоже самое что 

/*var xml_file = File.CreateText(xml_file_name);
try
{
    xml_sirialiser.Serialize(xml_file, student);
}
finally
{
    xml_file.Dispose();
}*/

Student? student2 = null;
using (var xml_file = File.OpenText(xml_file_name))
{
    student2 = (Student?)xml_sirialiser.Deserialize(xml_file);
};

    Console.ReadLine();
