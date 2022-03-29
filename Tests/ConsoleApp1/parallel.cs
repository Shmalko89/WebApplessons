using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1;

internal static class parallel
{

    public static void Run()
    {
        var messages = Enumerable.Range(1, 300).Select(i => $"message-{i}");

        var timer = Stopwatch.StartNew();

        /*var total_lenght = messages.AsParallel().WithDegreeOfParallelism(15).Select(m => ProcessMessage(m)).AsSequential().Sum();
         //foreach(var msg in messages)
            // total_lenght += ProcessMessage(msg);*/

        //Parallel.Invoke();
        //Parallel.For(5, 100, i => Console.WriteLine("$123 - {i}"));
        Parallel.ForEach(messages, str => ProcessMessage(str));
        timer.Stop();

        Console.WriteLine("$Обработка сообщений завершена за {0}", timer.Elapsed);
    }
    
    private static int ProcessMessage(string message)
    {
        Console.WriteLine("$Обработка сообщения {message}...");
        Thread.Sleep(10);
        Console.WriteLine("$Обработка сообщения {message} завершена");

        return message.Length;
    }
}
