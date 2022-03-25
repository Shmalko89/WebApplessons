
namespace ConsoleApp1
{
    public class ThreadPoolTests
    {
        public static void Run()
        {
            var messages = Enumerable.Range(1, 1000)
                .Select(i => $"Message-{i}");

            foreach(var message in messages)
                ThreadPool.QueueUserWorkItem(_ => ProcessMessage(message));

            Console.ReadLine();
        }

        private static void ProcessMessage (string Message, int Timeout = 250)
        {
            Console.WriteLine($"Запуск процесса обработки {Message} в потоке {Environment.CurrentManagedThreadId}");
            if (Timeout > 0)
                Thread.Sleep(Timeout);
            Console.WriteLine($"Gроцесса обработки {Message} в потоке {Environment.CurrentManagedThreadId} завершен");
        }

    }
}
