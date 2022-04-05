namespace ConsoleApp1;

public interface IStudentSerialiser
{
    void Serialise(TextWriter writer, List<Student> students);

    List<Student> DeSerialize(TextReader reader);


}
