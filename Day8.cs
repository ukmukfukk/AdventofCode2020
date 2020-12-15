using System;
using System.Collections.Generic;
using AdventOfCode2020.Day8Help;

namespace AdventOfCode2020
{
    public class Day8 : IDay
    {
        public IList<string> InputFiles => new List<string> { "input8.txt" };

        public string Name => "Day 8";

        public void SolvePuzzle1(IList<IList<string>> inputs)
        {
            SolvePuzzle1(inputs[0]);
        }

        public void SolvePuzzle2(IList<IList<string>> inputs)
        {
            SolvePuzzle2(inputs[0]);
        }

        private void SolvePuzzle2(IList<string> list)
        {
            List<Tuple<string, int>> commands = GetCommands(list);
            List<Tuple<string, int>> workcopy;
            LoopInfo li = new LoopInfo();
            int testid = 0;

            do
            {
                if (commands[testid].Item1 == "acc")
                {
                    testid++;
                    continue;
                }

                workcopy = MakeCopyOfCommands(commands);

                switch (workcopy[testid].Item1)
                {
                    case "jmp": workcopy[testid] = new Tuple<string, int>("nop", workcopy[testid].Item2); break;
                    case "nop": workcopy[testid] = new Tuple<string, int>("jmp", workcopy[testid].Item2); break;
                }

                li = TestForLoop(workcopy);
                testid++;
            }
            while (li.IsLoop);

            Helper.Logger.Log(Name, $"Acc: {li.Acc}");
        }

        private void SolvePuzzle1(IList<string> list)
        {
            List<Tuple<string, int>> commands = GetCommands(list);
            var li = TestForLoop(commands);

            Helper.Logger.Log(Name, $"Acc: {li.Acc}");
        }

        private List<Tuple<string, int>> MakeCopyOfCommands(List<Tuple<string, int>> commands)
        {
            List<Tuple<string, int>> workcopy = new List<Tuple<string, int>>();
            {
                foreach (var t in commands)
                {
                    workcopy.Add(new Tuple<string, int>(t.Item1, t.Item2));
                }
            }

            return workcopy;
        }

        private LoopInfo TestForLoop(List<Tuple<string, int>> commands)
        {
            var retval = new LoopInfo() { IsLoop = true };
            bool[] commandhit = new bool[commands.Count];

            int cmdN = 0;
            while (true)
            {
                if (cmdN >= commands.Count)
                {
                    retval.IsLoop = false;
                    break;
                }

                if (commandhit[cmdN])
                {
                    break;
                }

                commandhit[cmdN] = true;

                if (commands[cmdN].Item1 == "acc")
                {
                    retval.Acc += commands[cmdN].Item2;
                }

                if (commands[cmdN].Item1 == "jmp")
                {
                    cmdN += commands[cmdN].Item2;
                }
                else
                {
                    cmdN++;
                }
            }

            return retval;
        }

        private List<Tuple<string, int>> GetCommands(IList<string> list)
        {
            var retval = new List<Tuple<string, int>>();
            string temp;

            foreach (string s in list)
            {
                temp = s[0..1];
                retval.Add(new Tuple<string, int>(s[0..3], int.Parse(s[4..^0])));
            }

            return retval;
        }
    }
}
