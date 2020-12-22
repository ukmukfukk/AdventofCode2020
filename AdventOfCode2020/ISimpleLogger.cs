using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2020
{
    public interface ISimpleLogger : ILogger
    {
        public LogLevel MinLogLevel { get; }

        void Log(string message, LogLevel logLevel = LogLevel.Error);

        void Log(string name, string message, LogLevel logLevel = LogLevel.Error);

        void Log(Exception e, [CallerMemberName] string name = "", LogLevel logLevel = LogLevel.Error);
    }
}