

namespace ConsoleApp1
{
    internal static class ThreadTests
    {
        public static void Run()
        {
            var timer_thread = new Thread(TimerUpdate);
            timer_thread.Priority = ThreadPriority.BelowNormal;
            timer_thread.Name = "Поток обновления часов в заголовке консоли";
            timer_thread.IsBackground = true;
            timer_thread.Start();

            Console.WriteLine("Программа ожидает ввода пользователя");

            Console.ReadLine();

            _UpdateTitleTimer = false;
            if (!timer_thread.Join(300))
            {
                timer_thread.Interrupt();
                timer_thread.Join();
            }


            Console.WriteLine("Программа завершена!");
        }

        private static bool _UpdateTitleTimer = true;

        private static void TimerUpdate()
        {
            try
            {
                while (_UpdateTitleTimer)
                {
                    Console.Title = DateTime.Now.ToString("dd.MM.yyyy HH:mm.ss.ffff");

                    Thread.Sleep(250);
                }

                Console.WriteLine("Метод обновления часов завершен штатно");

            }
            catch (ThreadInterruptedException)
            {
                Console.WriteLine("Поток прерван мягко");
            }

        }
    }

    
}
