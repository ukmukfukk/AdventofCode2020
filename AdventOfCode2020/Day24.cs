using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode2020.Day24Help;

namespace AdventOfCode2020
{
    public class Day24 : DayUsingInputNames
    {
        public override IList<string> InputFiles => new List<string> { "input24test.txt", "input24.txt" };

        public override string Name => "Day 24";

        protected override void SolvePuzzle1(string inputname, IList<string> input)
        {
            List<List<Direction>> directions = ReadDirections(input);
            HexGrid hex = new HexGrid();
            hex.FollowDirections(directions);
            int blacks = hex.CountBlacks();
            Log($" part1 input {inputname} cnt: {blacks}");
        }

        protected override void SolvePuzzle2(string inputname, IList<string> input)
        {
            List<List<Direction>> directions = ReadDirections(input);
            HexGrid hex = new HexGrid();
            hex.FollowDirections(directions);

            for (int i = 0; i < 100; i++)
            {
                List<HexTile> turntiles = new List<HexTile>();
                foreach (HexTile tile in hex.AllTiles)
                {
                    int blacknb = tile.BlackNeighbours();
                    if (tile.Color == Color.White)
                    {
                        if (blacknb == 2)
                        {
                            turntiles.Add(tile);
                        }
                    }
                    else
                    {
                        if (blacknb == 0 || blacknb > 2)
                        {
                            turntiles.Add(tile);
                        }
                    }
                }

                foreach (HexTile tile in turntiles)
                {
                    tile.ChangeColor();
                }
            }

            int blacks = hex.CountBlacks();
            Log($" part1 input {inputname} cnt: {blacks}");
        }

        private List<List<Direction>> ReadDirections(IList<string> input)
        {
            var re = new List<List<Direction>>();
            foreach (string s in input.Select(s => s.ToUpper()))
            {
                var line = new List<Direction>();
                re.Add(line);
                int i = 0;
                while (i < s.Length)
                {
                    if (s[i].IsOneOf('N', 'S'))
                    {
                        line.Add(Enum.Parse<Direction>(s[i..(i + 2)].ToUpper()));
                        i += 2;
                    }
                    else
                    {
                        line.Add(Enum.Parse<Direction>(s[i..(i + 1)].ToUpper()));
                        i++;
                    }
                }
            }

            return re;
        }
    }
}
