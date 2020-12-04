using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2020
{
    internal interface ISimpleLogger : ILogger
    {
        void Log(string message);

        void Log(string name, string message);

        void Log(Exception e, [CallerMemberName] string name = "");
    }
}