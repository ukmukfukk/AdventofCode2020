using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2020
{
    public abstract class LoggingDay : IDay
    {
        public abstract IList<string> InputFiles { get; }

        public abstract string Name { get; }

        public abstract void SolvePuzzle1(IList<IList<string>> inputs);

        public abstract void SolvePuzzle2(IList<IList<string>> inputs);

        public virtual void Log(string message = "", LogLevel logLevel = LogLevel.Error)
        {
            Helper.Logger.Log($"{Name} {message}", logLevel);
        }

        public virtual void LogNoName(string message = "", LogLevel logLevel = LogLevel.Error)
        {
            Helper.Logger.Log($"{message}", logLevel);
        }
    }
}