using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day24Help
{
    public class HexGrid
    {
        private List<HexTile> allTiles = new List<HexTile>();

        public HexGrid()
        {
            Center = new HexTile();
            allTiles.Add(Center);
        }

        public List<HexTile> AllTiles { get => allTiles; }

        public HexTile Center { get; private set; }

        public void FollowDirections(List<List<Direction>> directions)
        {
            for (int i = 0; i < 100; i++)
            {
                var edges = allTiles.Where(t => t.IsEdge).ToList();
                foreach (HexTile tile in edges)
                {
                    if (tile.IsEdge)
                    {
                        tile.GenerateNeighbours(ref allTiles);
                    }
                }
            }

            foreach (var line in directions)
            {
                HexTile target = MoveDirections(line);
                target.ChangeColor();
            }
        }

        public int CountBlacks()
        {
            return allTiles.Count(t => t.Color == Color.Black);
        }

        private HexTile MoveDirections(List<Direction> line)
        {
            HexTile current = Center;
            foreach (Direction dir in line)
            {
                if (!current.Neighbours.ContainsKey(dir))
                {
                    throw new Exception("Grid is not enough");
                }

                current = current.Neighbours[dir];
            }

            return current;
        }
    }
}
