using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Day20Help
{
    public class SeaMonster
    {
        private readonly List<string> lines = new List<string>
        {
            "                  # ",
            "#    ##    ##    ###",
            " #  #  #  #  #  #   ",
        };

        public SeaMonster()
        {
            Cords = new List<Tuple<int, int>>();
            Width = lines[0].Length;
            Height = lines.Count;
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (lines[j][i] == '#')
                    {
                        Cords.Add(new Tuple<int, int>(j, i));
                    }
                }
            }
        }

        public int Width { get; }

        public int Height { get; }

        public List<Tuple<int, int>> Cords { get; set; }
    }
}
