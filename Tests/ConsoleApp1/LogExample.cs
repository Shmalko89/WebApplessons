

using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static ConsoleApp1.LogExample;

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
        /*if (ReferenceEquals(file_logger, first_file_logger))
            logger.Log("Файловый логгер был создан в еденичном экземпляре");

        logger.Log("123");
        logger.Log("qwerty");*/
        var messages = Enumerable.Range(1, 300).Select(i => $"message-{i}") ;

        var file_loggers_factory = new FileLoggerFactory();
        var debug_logger_factory = new DebugLoggerFactory();
        debug_logger_factory.OutTo = DebugLoggerFactory.LoggerType.Console;
        var rnd = new Random();

        var processor = new MessagePpocessor(rnd.NextDouble() > 0.5 ? file_loggers_factory : debug_logger_factory);
        processor.Process(messages);
    }

    private class MessagePpocessor
    {
        private readonly ILoggerFactory _Loggers;
        public MessagePpocessor(ILoggerFactory Loggers) { _Loggers = Loggers;}

        public void Process(IEnumerable<string> messages)
        {
            var logger = _Loggers.Create();
            foreach(var msg in messages)
                logger.Log(msg);
        }
    }
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

    internal class DebugLogger : Logger
    {
        
        public override void Log(string message) => Debug.WriteLine($"{DateTime.Now:yyyy-MM-ddTHH:mm:ss.ffff} - {message}");

    }

    internal class TraceLogger : Logger
    {

        public override void Log(string message) => Trace.WriteLine($"{DateTime.Now:yyyy-MM-ddTHH:mm:ss.ffff} - {message}");

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


internal interface ILoggerFactory
{
    ILogger Create();
}

abstract class LoggerFactory : ILoggerFactory
{
    public abstract ILogger Create();
}

internal class DebugLoggerFactory : LoggerFactory
{
    public enum LoggerType
    {
        Console,
        Debug,
        Trace
    }
    public LoggerType OutTo { get; set; }

    //новая реализация switch
    public override ILogger Create() => OutTo switch
        {
            LoggerType.Console => ConsoleLogger.Logger,
            LoggerType.Debug => new DebugLogger(),
            LoggerType.Trace => new TraceLogger(),
            _ => throw new InvalidEnumArgumentException(nameof(OutTo), (int)OutTo, typeof(LoggerType))
        };

        /*
        switch (OutTo)
        {
            default: throw new InvalidEnumArgumentException(nameof(OutTo), (int)OutTo, typeof(LoggerType));
            case LoggerType.Console: return ConsoleLogger.Logger; 
            case LoggerType.Debug: return new DebugLogger(); 
            case LoggerType.Trace: return new TraceLogger(); 

        }*/ // старая реализация switch
}

internal class FileLoggerFactory : LoggerFactory
{
    public override ILogger Create()
    {
      
    }
}
