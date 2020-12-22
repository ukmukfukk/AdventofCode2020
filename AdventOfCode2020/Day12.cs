using System;
using System.Collections.Generic;

namespace AdventOfCode2020
{
    public class Day12 : IDay
    {
        public IList<string> InputFiles => new List<string> { "input12test.txt", "input12.txt" };

        public string Name => "Day 12";

        public void SolvePuzzle1(IList<IList<string>> inputs)
        {
            foreach (var input in inputs)
            {
                SolvePuzzle1(input);
            }
        }

        public void SolvePuzzle2(IList<IList<string>> inputs)
        {
            foreach (var input in inputs)
            {
                SolvePuzzle2(input);
            }
        }

        private void SolvePuzzle1(IList<string> input)
        {
            int posx = 0;
            int posy = 0;
            char dir = 'E';

            MoveShip(ref posx, ref posy, ref dir, input);

            Helper.Logger.Log(Name, $"Position: {posx},{posy}");
        }

        private void MoveShip(ref int posx, ref int posy, ref char dir, IList<string> input)
        {
            char command;
            int p;

            for (int i = 0; i < input.Count; i++)
            {
                command = input[i][0];
                p = int.Parse(input[i][1..]);
                switch (command)
                {
                    case 'N':
                    case 'S':
                    case 'E':
                    case 'W': Move(ref posx, ref posy, command, p); break;
                    case 'L':
                    case 'R': Turn(ref dir, command, p); break;
                    case 'F': Move(ref posx, ref posy, dir, p); break;
                    default: throw new Exception("Unexpected");
                }
            }
        }

        private void Turn(ref char dir, char command, int p)
        {
            var angle = dir switch
            {
                'N' => 0,
                'S' => 180,
                'E' => 90,
                'W' => 270,
                _ => throw new Exception("Unexpected"),
            };
            if (p % 90 != 0)
            {
                throw new Exception("turn not by 90");
            }

            if (command == 'R')
            {
                angle += p;
            }
            else
            {
                angle -= p;
            }

            while (angle < 0)
            {
                angle += 360;
            }

            angle %= 360;

            dir = angle switch
            {
                0 => 'N',
                90 => 'E',
                180 => 'S',
                270 => 'W',
                _ => throw new Exception("Unexpected"),
            };
        }

        private void Move(ref int posx, ref int posy, char dir, int p)
        {
            switch (dir)
            {
                case 'N': posy -= p; break;
                case 'S': posy += p; break;
                case 'E': posx += p; break;
                case 'W': posx -= p; break;
            }
        }

        private void SolvePuzzle2(IList<string> input)
        {
            int posx = 0;
            int posy = 0;
            int wpposx = 10;
            int wpposy = -1;

            MoveShip2(ref posx, ref posy, ref wpposx, ref wpposy, input);

            Helper.Logger.Log(Name, $"Position: {posx},{posy}");
        }

        private void MoveShip2(ref int posx, ref int posy, ref int wpposx, ref int wpposy, IList<string> input)
        {
            char command;
            int p;

            for (int i = 0; i < input.Count; i++)
            {
                command = input[i][0];
                p = int.Parse(input[i][1..]);
                switch (command)
                {
                    case 'N':
                    case 'S':
                    case 'E':
                    case 'W': Move(ref wpposx, ref wpposy, command, p); break;
                    case 'L': Turn2(ref wpposx, ref wpposy, -1 * p); break;
                    case 'R': Turn2(ref wpposx, ref wpposy, p); break;
                    case 'F': Move2(ref posx, ref posy, wpposx, wpposy, p); break;
                    default: throw new Exception("Unexpected");
                }
            }
        }

        private void Move2(ref int posx, ref int posy, int wpposx, int wpposy, int p)
        {
            posx += wpposx * p;
            posy += wpposy * p;
        }

        private void Turn2(ref int wpposx, ref int wpposy, int dir)
        {
            dir %= 360;
            if (dir < 0)
            {
                dir += 360;
            }

            if (dir >= 180)
            {
                wpposx *= -1;
                wpposy *= -1;
                dir -= 180;
            }

            if (dir == 90)
            {
                int temp = wpposy * -1;
                wpposy = wpposx;
                wpposx = temp;
                dir -= 90;
            }

            if (dir != 0)
            {
                throw new Exception("Something went wrong");
            }
        }
    }
}
