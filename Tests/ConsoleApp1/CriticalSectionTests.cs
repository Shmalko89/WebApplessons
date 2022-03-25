

namespace ConsoleApp1
{
    internal class CriticalSectionTests
    {
        public static void Run()
        {
            var thread_list = new List<Thread>();

            for (var i = 0; i < 10; i++)
            {
                var i0 = i;
                var thread = new Thread(() => Print($"Message-{i0}", 50));
                thread.IsBackground = true;
                thread_list.Add(thread);
                thread.Start();
            }
            Console.ReadLine();
        }

        private static void Print(string Message,int Count, int Timeout = 20)
        {
            for(var i = 0; i < Count; i++)
            {
                if(Timeout > 0)
                Thread.Sleep(Timeout);

                Console.Write("ThreadID: {0}", Environment.CurrentManagedThreadId);
                Console.Write("[{0}] ", i);
                Console.WriteLine(Message);
                
            }
        }
    }
}
