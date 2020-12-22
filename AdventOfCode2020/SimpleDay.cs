using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2020
{
    public abstract class SimpleDay : IDay
    {
        public abstract IList<string> InputFiles { get; }

        public abstract string Name { get; }

        public virtual void SolvePuzzle1(IList<IList<string>> inputs)
        {
            foreach (var input in inputs)
            {
                SolvePuzzle1(input);
            }
        }

        public virtual void SolvePuzzle2(IList<IList<string>> inputs)
        {
            foreach (var input in inputs)
            {
                SolvePuzzle2(input);
            }
        }

        public virtual void Log(string message = "", LogLevel logLevel = LogLevel.Error)
        {
            Helper.Logger.Log($"{Name} {message}", logLevel);
        }

        public virtual void LogNoName(string message = "", LogLevel logLevel = LogLevel.Error)
        {
            Helper.Logger.Log($"{message}", logLevel);
        }

        protected abstract void SolvePuzzle1(IList<string> input);

        protected abstract void SolvePuzzle2(IList<string> input);
    }
}
