using System;
using System.Collections.Generic;
using System.Reflection;

namespace AdventOfCode2020
{
    public class Program
    {
        // .\bin\netcoreapp3.1\AdventOfCode2020.exe "Inputs" 1 1
        private static void Main(string[] args)
        {
            Helper.Logger = new ConsoleLogger();

            try
            {
                if (args.Length < 3)
                {
                    throw new Exception("Did not get expected number of params, which is 3");
                }

                if (!int.TryParse(args[1], out int dayNo))
                {
                    throw new Exception("Param 1 is day number, should be int");
                }

                if (!int.TryParse(args[2], out int puzzleNo))
                {
                    throw new Exception("Param 2 is puzzle number, should be int");
                }

                IDay day = InstantiateDay(dayNo);
                var inputs = Helper.GetInputs(args[0], day.InputFiles);
                SolvePuzzle(puzzleNo, day, inputs);
            }
            catch (Exception e)
            {
                Helper.Logger.Log(e);
            }
        }

        private static void SolvePuzzle(int puzzleNo, IDay day, IList<IList<string>> inputs)
        {
            switch (puzzleNo)
            {
                case 1: day.SolvePuzzle1(inputs); break;
                case 2: day.SolvePuzzle2(inputs); break;
                default: throw new Exception($"No puzzle for puzzle number {puzzleNo}");
            }
        }

        private static IDay InstantiateDay(int dayNo)
        {
            string typeString = $"AdventOfCode2020.Day{dayNo}";
            Type type = Assembly.GetExecutingAssembly().GetType(typeString);
            return (IDay)Activator.CreateInstance(type);
        }
    }
}
