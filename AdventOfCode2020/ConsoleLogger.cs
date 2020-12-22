using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2020
{
    internal class ConsoleLogger : ISimpleLogger
    {
        private readonly Func<string, Exception, string> formatter;

        public ConsoleLogger()
        {
            formatter = (s, e) => e.Message;
            MinLogLevel = LogLevel.Warning;
        }

        public ConsoleLogger(LogLevel logLevel)
        {
            formatter = (s, e) => e.Message;
            MinLogLevel = logLevel;
        }

        public LogLevel MinLogLevel { get; }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= MinLogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (IsEnabled(logLevel))
            {
                Console.WriteLine(formatter.Invoke(state, exception));
            }
        }

        public void Log(string message, LogLevel logLevel = LogLevel.Error)
        {
            if (IsEnabled(logLevel))
            {
                Log(logLevel, new EventId(0), string.Empty, new Exception($"{message}"), formatter);
            }
        }

        public void Log(string name, string message, LogLevel logLevel = LogLevel.Error)
        {
            if (IsEnabled(logLevel))
            {
                Log(logLevel, new EventId(0), string.Empty, new Exception($"{name}{(string.IsNullOrEmpty(name) ? string.Empty : " ")}{message}"), formatter);
            }
        }

        public void Log(Exception e, [CallerMemberName] string name = "", LogLevel logLevel = LogLevel.Error)
        {
            if (IsEnabled(logLevel))
            {
                Log(logLevel, new EventId(0), string.Empty, new Exception($"{name}{(string.IsNullOrEmpty(name) ? string.Empty : " ")}{e.Message}", e), formatter);
                Log(logLevel, new EventId(0), string.Empty, new Exception($"{e.StackTrace}"), formatter);
            }
        }
    }
}
