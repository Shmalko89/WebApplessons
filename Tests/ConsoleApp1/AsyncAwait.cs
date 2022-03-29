using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1;

internal class AsyncAwait
{
    public static void Run()
    {
        //Task task = new Task(() => PrintMessage("123")); //так не делают
        // task.Start();

        var print_message_task = Task.Run(() => PrintMessage("123"));
        //print_message_task.Status == 
        
        Task.Factory.StartNew(() => PrintMessage("123", TaskCreationOptions.LongRunning)

    }

    private static int ProcessMessage(string message)
    {
        Console.WriteLine("$Обработка сообщения {message}...");
        Thread.Sleep(10);
        Console.WriteLine("$Обработка сообщения {message} завершена");

        return message.Length;
    }

    private static void PrintMessage(string message, int Timeout = 10)
    {
        Console.WriteLine("$Обработка сообщения {message}...");
        if(Timeout > 0)
            Thread.Sleep(Timeout);
        Console.WriteLine("$Обработка сообщения {message} завершена");
    }
}
