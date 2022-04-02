

using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace ConsoleApp1;

internal class LogExample
{
    public static void Run()
    {
        var log_filepath = $"TestConsole[{DateTime.Now:yyyy-MM-ddTHH-mm-ss}].log";
        var logger = new CombineLogger(
            ConsoleLogger.Logger,
            FileLogger.Create(log_filepath));
        var file_logger = FileLogger.Create(log_filepath);
        var first_file_logger = logger.Loggers.OfType<FileLogger>().First();
        if (ReferenceEquals(file_logger, first_file_logger))
            logger.Log("Файловый логгер был создан в еденичном экземпляре");

        logger.Log("123");
        logger.Log("qwerty");
    }


    internal interface ILogger
    {
        void Log(string message);
    }

    internal abstract class Logger: ILogger
    {
       
        public abstract void Log(string message);
    }

    internal class ConsoleLogger : Logger
    {
        private static ConsoleLogger? __Logger;
        //реализация шаблона обиночка с ленивой реализацией без многопоточности
        //public static ConsoleLogger Logger => __Logger ??= new();
        //Для многопоточности
        private static readonly object __LoggerSyncRoot = new();
        public static ConsoleLogger Logger
        {
            get
            {
                if(__Logger != null) return __Logger;
                lock (__LoggerSyncRoot)
                {
                    if (__Logger != null) return __Logger;
                    __Logger = new();
                    return __Logger;
                }
            }
        }

        //реализация проще

        private static readonly Lazy<ConsoleLogger> __LazyLogger = new(() => new(), LazyThreadSafetyMode.ExecutionAndPublication);

        private ConsoleLogger()
        {

        }

        public override void Log(string message) => Console.WriteLine($"{DateTime.Now:yyyy-MM-ddTHH:mm:ss.ffff} - {message}");
       
    }

    internal class FileLogger : Logger
    {
        private static readonly ConcurrentDictionary<string, FileLogger> __Loggers = new(StringComparer.OrdinalIgnoreCase);

        public static FileLogger Create(string fileName)
        {
            return __Loggers.GetOrAdd(fileName, path => new FileLogger(path));
        }
        private string _fileName;

        private FileLogger(string fileName) => _fileName = fileName;

        [MethodImpl(MethodImplOptions.Synchronized)] //Блокривка, пускает только один поток
        public override void Log(string message)
        {
            File.AppendAllText(_fileName, $"{DateTime.Now:s} - {message}{Environment.NewLine}");
        }

        
    }
    internal class CombineLogger : Logger
    {
        private readonly ILogger[] _Loggers;

        public IReadOnlyList<ILogger> Loggers => _Loggers;
        public CombineLogger(params ILogger[] Loggers) => _Loggers = Loggers;

        public override void Log(string message)
        {
            foreach (var logger in _Loggers)
                logger.Log(message);
        }
    }
}
