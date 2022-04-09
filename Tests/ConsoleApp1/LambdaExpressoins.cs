using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1;

internal class LambdaExpressoins
{
    public static void Run()
    {
        string str = "123";
        int x = 42;
        List<int> ints = new();
        Student student = new();
        Process(student);

        StudentProcessor process = Free;

        ProcessStudent(Enumerable.Empty<Student>(), Process);

        Action<Student> processor2 = Free;
        Func<Student, int> metric = s => s.LastName.Length;

        Func<double, double> sin = x =>Math.Sin(x);
        Func<double, double> cos = x => Math.Cos(x);

        Func<double, double> sum_sin_cos = Sum(sin, cos);

        var resulr = sum_sin_cos(Math.PI / 3);
    }

    public static Func<double, double> Sum (Func<double, double> a, Func<double, double> b)
    {
        return x => a(x) + b(x);
    }

    public delegate void StudentProcessor(Student student);

    public static void ProcessStudent (IEnumerable<Student> student, StudentProcessor processor /*делегат*/)
    {
        foreach(var std in student)
            processor(std);
    }

    public static void Process(Student student)
    {
        Console.WriteLine(student);
    }

    public static void Free (Student student)
    {
        Console.WriteLine(student);
    }
}
