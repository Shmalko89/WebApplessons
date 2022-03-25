

namespace ConsoleApp1
{
    internal static class ThreadTests
    {
        public static void Run()
        {
            while (true)
            {
                Console.Title = DateTime.Now.ToString("dd.MM.yyyy HH:mm.ss.ffff");

                Thread.Sleep(250);
            }


            Console.WriteLine("Программа ожидает ввода пользователя");

            Console.ReadLine();

            Console.WriteLine("Программа завершена!");
        }
    }
}
