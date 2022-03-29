using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Security.Cryptography;

namespace ConsoleApp1;

internal static class ThreadSafeDictionary
{
    public static void Run()
    {
        var strings = Enumerable.Range(1, 1000).Select(i => $"String -{i % 54}");

        foreach (var str in strings)
            ThreadPool.QueueUserWorkItem(_ =>
            {
                var hash = ComputeHashBuffered(str);
                Console.WriteLine("{0} : {1}", str, Convert.ToBase64String(hash));
            });

        Console.ReadLine();
    }

    private static readonly ConcurrentDictionary<string, byte[]> __HashBuffer = new();

    private static byte[] ComputeHashBuffered(string str) => __HashBuffer.GetOrAdd(str, s => ComputeHash(s));
  
    private static byte[] ComputeHash(string str)
    {


        if(str is null) throw new ArgumentNullException(nameof(str));
        if (str.Length == 0) return new byte[32];

        Thread.Sleep(1000);

        var bytes = Encoding.UTF8.GetBytes(str);
        using var hasher = MD5.Create();
        return hasher.ComputeHash(bytes);
    }
}
