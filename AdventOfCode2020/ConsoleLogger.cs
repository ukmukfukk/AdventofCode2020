using System;
using System.Diagnostics;
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
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Console.WriteLine(formatter.Invoke(state, exception));
        }

        public void Log(string message)
        {
            Log(LogLevel.Information, new EventId(0), string.Empty, new Exception($"{message}"), formatter);
        }

        public void Log(string name, string message)
        {
            Log(LogLevel.Information, new EventId(0), string.Empty, new Exception($"{name}{(string.IsNullOrEmpty(name) ? string.Empty : " ")}{message}"), formatter);
        }

        public void Log(Exception e, [CallerMemberName] string name = "")
        {
            Log(LogLevel.Information, new EventId(0), string.Empty, new Exception($"{name}{(string.IsNullOrEmpty(name) ? string.Empty : " ")}{e.Message}", e), formatter);
            Log(LogLevel.Information, new EventId(0), string.Empty, new Exception($"{e.StackTrace}"), formatter);
        }
    }
}
