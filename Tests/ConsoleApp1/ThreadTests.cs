

namespace ConsoleApp1
{
    internal static class ThreadTests
    {
        public static void Run()
        {
            var timer_thread = new Thread(TimerUpdate);


            timer_thread.Start();

            Console.WriteLine("Программа ожидает ввода пользователя");

            Console.ReadLine();

            Console.WriteLine("Программа завершена!");
        }

        private static void TimerUpdate()
        {
            while (true)
            {
                Console.Title = DateTime.Now.ToString("dd.MM.yyyy HH:mm.ss.ffff");

                Thread.Sleep(250);
            }
        }
    }

    
}
