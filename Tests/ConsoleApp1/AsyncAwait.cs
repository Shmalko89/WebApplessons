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

        Task.Factory.StartNew(() => PrintMessage("123"), TaskCreationOptions.LongRunning);

    }

    private static int ProcessMessage(string message)
    {
        Console.WriteLine("$Обработка сообщения {message}...");
        Thread.Sleep(10);
        Console.WriteLine("$Обработка сообщения {message} завершена");

        return message.Length;
    }



    /*private static Task<int> ProcessMessageAsync(string message)
    {
        Console.WriteLine("$Обработка сообщения {message}...");
        Thread.Sleep(10);
        Console.WriteLine("$Обработка сообщения {message} завершена");

        return Task.FromResult(message.Length);
    }*/

    private static async Task<int> ProcessMessageAsync(string message)
    {
        await Task.Yield(); // читерский способ
        Console.WriteLine("$Обработка сообщения {message}...");
        //Thread.Sleep(10); в асинхронных методах не должно быть усыпления потока
        await Task.Delay(10).ConfigureAwait(false); //false - продолжение будет выполнено в одном из потоков пула потоков
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

    public static async Task RunAsync()
    {
        var message = "123";

        var process_message_task = Task.Run(() => ProcessMessage(message));

        Console.WriteLine("Код выполняемый в главном потоке запуска задачи");
        try
        {
            var result = await process_message_task; // правильная реализация завершения задачи вместо process_message_task.Wait()

            Console.WriteLine("$Результат выполнения задачи составил {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
        
    }

    public static async Task Run2()
    {
        var messages = Enumerable.Range(1, 300).Select(i => $"message-{i}");
        //var task_list = new List<Task>();
        //foreach (var msg in messages)
        //  task_list.Add(Task.Run(() => ProcessMessage(msg)));

        // var result = await Task.WhenAll(task_list);
        var result = await Task.WhenAll(messages.Select(m => ProcessMessageAsync(m)));
        Console.WriteLine($"Сумма = {result.Sum()}");
    }
}
