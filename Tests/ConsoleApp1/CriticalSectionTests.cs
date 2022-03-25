

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

        private static readonly object __SyncRoot = new object();


        private static void Print(string Message,int Count, int Timeout = 20)
        {
            for(var i = 0; i < Count; i++)
            {
                if(Timeout > 0)
                Thread.Sleep(Timeout);


                lock (__SyncRoot)
                {
                    #region Критическая секция
                    Console.Write("ThreadID: {0}", Environment.CurrentManagedThreadId);
                    Console.Write("[{0}] ", i);
                    Console.WriteLine(Message);
                    #endregion
                }


            }
        }

        public static void Run2()
        {
            var messages = new List<string>();

            for (var i = 0; i < 25 ; i++)
            {
                var i0 = i;
                var thread = new Thread(() =>
                {
                    var thread_id = Environment.CurrentManagedThreadId;

                    for (var j = 0; j <50; j++)
                    {
                        var message = $"[{j}] Message-{i} from thread {thread_id}";

                        lock (messages) 
                        {
                            messages.Add(message);
                        }
                        
                    }
                    

                   
                });
               
                thread.IsBackground = true;
                thread.Start();
            }
            Console.ReadLine();
        }

    }
}
