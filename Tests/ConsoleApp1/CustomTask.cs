using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class CustomTask
    {
        public static void Run()
        {
            var calculation = new CalculationTask<string>(
    () =>
    {
        Console.WriteLine("Процент вычисления запущен");
        Thread.Sleep(3000);
        Console.WriteLine("Процент вычисления завершен");
        return "42";
    });

            calculation.Start();
            Console.WriteLine("Запустили процесс расчета...");

            var result = calculation.Result;

            Console.WriteLine("Результат = {0}", result);
            Console.ReadLine();
        }
    }
}
