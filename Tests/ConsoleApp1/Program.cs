using System.Threading;

//var current_thread = Thread.CurrentThread;

//current_thread.Priority = ThreadPriority.Lowest;

while (true)
{
    Console.Title = DateTime.Now.ToString("dd.MM.yyyy HH:mm.ss.ffff");
}


Console.WriteLine("Программа ожидает ввода пользователя");

Console.ReadLine();

Console.WriteLine("Программа завершена!");
