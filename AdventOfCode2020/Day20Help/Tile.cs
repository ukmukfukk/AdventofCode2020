using System;
using System.Collections.Generic;

namespace AdventOfCode2020.Day20Help
{
    public class Tile
    {
        private bool[] topEdge;

        private bool[] bottomEdge;

        private bool[] leftEdge;

        private bool[] rightEdge;

        private IList<Tile> variations;

        public Tile(int id, List<string> line)
        {
            Id = id;
            Height = line.Count;
            Width = line[0].Length;
            Map = new bool[Height, Width];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Map[i, j] = line[i][j] == '#';
                }
            }
        }

        public Tile(long id, bool[,] map)
        {
            Id = id;
            Map = map;
            Height = map.GetLength(0);
            Width = map.GetLength(1);
        }

        public long Id { get; }

        public bool[,] Map { get; }

        public int Height { get; }

        public int Width { get; }

        public bool Placed { get; internal set; }

        public bool[] TopEdge
        {
            get
            {
                if (topEdge == null)
                {
                    topEdge = new bool[Width];
                    for (int i = 0; i < Width; i++)
                    {
                        topEdge[i] = Map[0, i];
                    }
                }

                return topEdge;
            }
        }

        public bool[] BottomEdge
        {
            get
            {
                if (bottomEdge == null)
                {
                    bottomEdge = new bool[Width];
                    for (int i = 0; i < Width; i++)
                    {
                        bottomEdge[i] = Map[Height - 1, i];
                    }
                }

                return bottomEdge;
            }
        }

        public bool[] LeftEdge
        {
            get
            {
                if (leftEdge == null)
                {
                    leftEdge = new bool[Height];
                    for (int i = 0; i < Height; i++)
                    {
                        leftEdge[i] = Map[i, 0];
                    }
                }

                return leftEdge;
            }
        }

        public bool[] RightEdge
        {
            get
            {
                if (rightEdge == null)
                {
                    rightEdge = new bool[Height];
                    for (int i = 0; i < Height; i++)
                    {
                        rightEdge[i] = Map[i, Width - 1];
                    }
                }

                return rightEdge;
            }
        }

        public IList<Tile> Variations
        {
            get
            {
                if (variations == null)
                {
                    variations = new List<Tile>();
                    bool[,] map = new bool[Height, Width];
                    for (int i = 0; i < Height; i++)
                    {
                        for (int j = 0; j < Width; j++)
                        {
                            map[i, j] = Map[i, j];
                        }
                    }

                    Tile rotation = new Tile(Id, map);

                    for (int i = 0; i < 4; i++)
                    {
                        if (i != 0)
                        {
                            rotation = new Tile(Id, Rotate(rotation.Map));
                        }

                        variations.Add(rotation);
                        variations.Add(new Tile(Id, FlipY(rotation.Map)));
                        variations.Add(new Tile(Id, FlipX(rotation.Map)));
                    }
                }

                return variations;
            }
        }

        private static bool[,] Rotate(bool[,] map)
        {
            bool[,] re = new bool[map.GetLength(1), map.GetLength(0)];
            for (int i = 0; i < map.GetLength(1); i++)
            {
                for (int j = 0; j < map.GetLength(0); j++)
                {
                    re[i, j] = map[map.GetLength(0) - j - 1, i];
                }
            }

            return re;
        }

        private static bool[,] FlipY(bool[,] map)
        {
            bool[,] re = new bool[map.GetLength(0), map.GetLength(1)];
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    re[i, j] = map[map.GetLength(0) - i - 1, j];
                }
            }

            return re;
        }

        private static bool[,] FlipX(bool[,] map)
        {
            bool[,] re = new bool[map.GetLength(0), map.GetLength(1)];
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    re[i, j] = map[i, map.GetLength(1) - j - 1];
                }
            }

            return re;
        }
    }
}
