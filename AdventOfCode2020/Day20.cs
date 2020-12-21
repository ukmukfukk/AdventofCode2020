using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode2020.Day20Help;

namespace AdventOfCode2020
{
    public class Day20 : SimpleDay
    {
        public override IList<string> InputFiles => new List<string> { "input20test.txt", "input20.txt" };

        public override string Name => "Day 20";

        public bool HasSeaMonster(Tile tile, SeaMonster seaMonster)
        {
            for (int i = 0; i <= tile.Map.GetLength(0) - seaMonster.Height; i++)
            {
                for (int j = 0; j <= tile.Map.GetLength(1) - seaMonster.Width; j++)
                {
                    if (MatchSeaMonster(tile, i, j, seaMonster))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        protected override void SolvePuzzle1(IList<string> input)
        {
            List<Tile> tiles = ReadTiles(input);
            Tile[,] tilemap = ResolveMap(tiles);
            Tuple<int, int> topleft = null;
            Tuple<int, int> bottomright = null;
            FindCorners(tilemap, ref topleft, ref bottomright);

            long multi = tilemap[topleft.Item1, topleft.Item2].Id *
                tilemap[bottomright.Item1, topleft.Item2].Id *
                tilemap[topleft.Item1, bottomright.Item2].Id *
                tilemap[bottomright.Item1, bottomright.Item2].Id;

            Helper.Logger.Log(Name, $"sum: {multi}");
        }

        protected override void SolvePuzzle2(IList<string> input)
        {
            List<Tile> tiles = ReadTiles(input);
            Tile[,] tilemap = ResolveMap(tiles);
            Tuple<int, int> topleft = null;
            Tuple<int, int> bottomright = null;
            FindCorners(tilemap, ref topleft, ref bottomright);

            int tileCountVertical = bottomright.Item1 - topleft.Item1 + 1;
            int tileCountHorizontal = bottomright.Item2 - topleft.Item2 + 1;
            int tileHeight = tilemap[topleft.Item1, topleft.Item2].Height - 2;
            int tileWidth = tilemap[topleft.Item1, topleft.Item2].Width - 2;
            bool[,] map = new bool[tileCountVertical * tileHeight, tileCountHorizontal * tileWidth];
            for (int i = 0; i < tileCountVertical; i++)
            {
                for (int j = 0; j < tileCountHorizontal; j++)
                {
                    Tile tile = tilemap[i + topleft.Item1, j + topleft.Item2];
                    for (int k = 0; k < tileHeight; k++)
                    {
                        for (int l = 0; l < tileWidth; l++)
                        {
                            map[(i * tileHeight) + k, (j * tileWidth) + l] = tile.Map[k + 1, l + 1];
                        }
                    }
                }
            }

            Tile supertile = new Tile(0, map);

            SeaMonster seaMonster = new SeaMonster();
            Tile variant = supertile.Variations[0];
            int v = 0;
            while (!HasSeaMonster(variant, seaMonster))
            {
                PrintMap(variant);
                Console.WriteLine();

                v++;
                if (v == supertile.Variations.Count)
                {
                    throw new Exception("No seamonster found");
                }

                variant = supertile.Variations[v];
            }

            ReplaceSeamonsters(variant, seaMonster);
            int count = 0;
            for (int i = 0; i < variant.Height; i++)
            {
                for (int j = 0; j < variant.Width; j++)
                {
                    if (variant.Map[i, j])
                    {
                        count++;
                    }
                }
            }

            Helper.Logger.Log(Name, $"count: {count}");
        }

        private static void PrintMap(Tile tile)
        {
            for (int i = 0; i < tile.Height; i++)
            {
                for (int j = 0; j < tile.Width; j++)
                {
                    Console.Write(tile.Map[i, j] ? "#" : " ");
                }

                Console.WriteLine();
            }
        }

        private void ReplaceSeamonsters(Tile tile, SeaMonster seaMonster)
        {
            for (int i = 0; i < tile.Map.GetLength(0) - seaMonster.Height; i++)
            {
                for (int j = 0; j < tile.Map.GetLength(1) - seaMonster.Width; j++)
                {
                    if (MatchSeaMonster(tile, i, j, seaMonster))
                    {
                        ReplaceSeamonster(tile, i, j, seaMonster);
                    }
                }
            }
        }

        private void ReplaceSeamonster(Tile tile, int i, int j, SeaMonster seaMonster)
        {
            foreach (var cord in seaMonster.Cords)
            {
                tile.Map[i + cord.Item1, j + cord.Item2] = false;
            }
        }

        private bool MatchSeaMonster(Tile tile, int i, int j, SeaMonster seaMonster)
        {
            foreach (var cord in seaMonster.Cords)
            {
                if (!tile.Map[i + cord.Item1, j + cord.Item2])
                {
                    return false;
                }
            }

            return true;
        }

        private void FindCorners(Tile[,] tilemap, ref Tuple<int, int> topleft, ref Tuple<int, int> bottomright)
        {
            int i = 0;
            int j = 0;
            bool found = false;
            while (!found)
            {
                if (tilemap[i, j] != null)
                {
                    if (topleft == null)
                    {
                        topleft = new Tuple<int, int>(i, j);
                    }
                    else
                    {
                        if ((j == tilemap.GetLength(1) - 1 && i == tilemap.GetLength(0) - 1) || (tilemap[i, j + 1] == null && tilemap[i + 1, j] == null))
                        {
                            bottomright = new Tuple<int, int>(i, j);
                            found = true;
                        }
                    }
                }

                j++;
                if (j == tilemap.GetLength(1))
                {
                    j = 0;
                    i++;
                }
            }
        }

        private Tile[,] ResolveMap(List<Tile> tiles)
        {
            Tile[,] re = new Tile[289, 289];
            re[144, 144] = tiles[0];
            tiles[0].Placed = true;
            FindNeighbours(re, tiles, 144, 144);

            return re;
        }

        private void FindNeighbours(Tile[,] map, List<Tile> tiles, int x, int y)
        {
            bool[] topedge = map[x, y].TopEdge;
            bool[] botomedge = map[x, y].BottomEdge;
            bool[] leftedge = map[x, y].LeftEdge;
            bool[] rightedge = map[x, y].RightEdge;
            foreach (Tile tile in tiles.Where(t => !t.Placed))
            {
                if (y > 0 && map[x, y - 1] == null)
                {
                    foreach (Tile variation in tile.Variations)
                    {
                        if (EdgeMatch(leftedge, variation.RightEdge))
                        {
                            map[x, y - 1] = variation;
                            tile.Placed = true;
                            FindNeighbours(map, tiles, x, y - 1);
                            break;
                        }
                    }
                }

                if (y < map.GetLength(1) - 1 && map[x, y + 1] == null)
                {
                    foreach (Tile variation in tile.Variations)
                    {
                        if (EdgeMatch(rightedge, variation.LeftEdge))
                        {
                            map[x, y + 1] = variation;
                            tile.Placed = true;
                            FindNeighbours(map, tiles, x, y + 1);
                            break;
                        }
                    }
                }

                if (x > 0 && map[x - 1, y] == null)
                {
                    foreach (Tile variation in tile.Variations)
                    {
                        if (EdgeMatch(topedge, variation.BottomEdge))
                        {
                            map[x - 1, y] = variation;
                            tile.Placed = true;
                            FindNeighbours(map, tiles, x - 1, y);
                            break;
                        }
                    }
                }

                if (x < map.GetLength(0) - 1 && map[x + 1, y] == null)
                {
                    foreach (Tile variation in tile.Variations)
                    {
                        if (EdgeMatch(botomedge, variation.TopEdge))
                        {
                            map[x + 1, y] = variation;
                            tile.Placed = true;
                            FindNeighbours(map, tiles, x + 1, y);
                            break;
                        }
                    }
                }
            }
        }

        private bool EdgeMatch(bool[] one, bool[] other)
        {
            if (one.Length != other.Length)
            {
                return false;
            }

            for (int i = 0; i < one.Length; i++)
            {
                if (one[i] != other[i])
                {
                    return false;
                }
            }

            return true;
        }

        private int[,] ReadExpected(IList<string> input)
        {
            int i = 0;
            while (i < input.Count && !input[i].Equals("Expected"))
            {
                i++;
            }

            if (i == input.Count)
            {
                return null;
            }

            i++;

            int[,] retval = new int[input.Count - i, input.Count - i];
            for (int k = 0; k + i < input.Count; k++)
            {
                string[] line = input[k + i].Split("    ");
                for (int j = 0; j < line.Length; j++)
                {
                    retval[k, j] = int.Parse(line[j]);
                }
            }

            return retval;
        }

        private List<Tile> ReadTiles(IList<string> input)
        {
            List<Tile> retval = new List<Tile>();
            int i = 0;
            while (i < input.Count && !input[i].Equals("Expected"))
            {
                int id = int.Parse(Helper.Digits.Match(input[i]).Value);
                i++;
                List<string> line = new List<string>();
                while (i < input.Count && !string.IsNullOrEmpty(input[i]))
                {
                    line.Add(input[i]);
                    i++;
                }

                retval.Add(new Tile(id, line));
                i++;
            }

            return retval;
        }
    }
}
