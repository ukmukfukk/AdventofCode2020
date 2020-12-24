using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Day24Help
{
    public class HexTile
    {
        private static int maxId = 0;

        public HexTile()
        {
            maxId++;
            Id = maxId;
        }

        public int Id { get; private set; }

        public Dictionary<Direction, HexTile> Neighbours { get; private set; } = new Dictionary<Direction, HexTile>();

        public Color Color { get; private set; } = Color.White;

        public bool HasAllNeighbours => Neighbours.Count == 6;

        public bool IsEdge => Neighbours.Count < 6;

        public void ChangeColor()
        {
            if (Color == Color.White)
            {
                Color = Color.Black;
            }
            else
            {
                Color = Color.White;
            }
        }

        public override string ToString()
        {
            return $"Id: {Id}, Edge: {IsEdge}";
        }

        internal void GenerateNeighbours(ref List<HexTile> allTiles)
        {
            if (HasAllNeighbours)
            {
                return;
            }

            HexTile eTile;
            HexTile seTile;
            HexTile swTile;
            HexTile wTile;
            HexTile nwTile;
            HexTile neTile;

            if (Neighbours.ContainsKey(Direction.E))
            {
                eTile = Neighbours[Direction.E];
            }
            else
            {
                eTile = new HexTile();
                allTiles.Add(eTile);
                Neighbours.Add(Direction.E, eTile);
                eTile.SetNeighbour(Direction.W, this);
            }

            if (Neighbours.ContainsKey(Direction.SE))
            {
                seTile = Neighbours[Direction.SE];
            }
            else
            {
                seTile = new HexTile();
                allTiles.Add(seTile);
                Neighbours.Add(Direction.SE, seTile);
                seTile.SetNeighbour(Direction.NW, this);
            }

            seTile.SetNeighbour(Direction.NE, eTile);
            eTile.SetNeighbour(Direction.SW, seTile);

            if (Neighbours.ContainsKey(Direction.SW))
            {
                swTile = Neighbours[Direction.SW];
            }
            else
            {
                swTile = new HexTile();
                allTiles.Add(swTile);
                Neighbours.Add(Direction.SW, swTile);
                swTile.SetNeighbour(Direction.NE, this);
            }

            swTile.SetNeighbour(Direction.E, seTile);
            seTile.SetNeighbour(Direction.W, swTile);

            if (Neighbours.ContainsKey(Direction.W))
            {
                wTile = Neighbours[Direction.W];
            }
            else
            {
                wTile = new HexTile();
                allTiles.Add(wTile);
                Neighbours.Add(Direction.W, wTile);
                wTile.SetNeighbour(Direction.E, this);
            }

            wTile.SetNeighbour(Direction.SE, swTile);
            swTile.SetNeighbour(Direction.NW, wTile);

            if (Neighbours.ContainsKey(Direction.NW))
            {
                nwTile = Neighbours[Direction.NW];
            }
            else
            {
                nwTile = new HexTile();
                allTiles.Add(nwTile);
                Neighbours.Add(Direction.NW, nwTile);
                nwTile.SetNeighbour(Direction.SE, this);
            }

            nwTile.SetNeighbour(Direction.SW, wTile);
            wTile.SetNeighbour(Direction.NE, nwTile);

            if (Neighbours.ContainsKey(Direction.NE))
            {
                neTile = Neighbours[Direction.NE];
            }
            else
            {
                neTile = new HexTile();
                allTiles.Add(neTile);
                Neighbours.Add(Direction.NE, neTile);
                neTile.SetNeighbour(Direction.SW, this);
            }

            neTile.SetNeighbour(Direction.W, nwTile);
            nwTile.SetNeighbour(Direction.E, neTile);

            eTile.SetNeighbour(Direction.NW, neTile);
            neTile.SetNeighbour(Direction.SE, eTile);
        }

        internal int BlackNeighbours()
        {
            return Neighbours.Count(nb => nb.Value.Color == Color.Black);
        }

        private void SetNeighbour(Direction dir, HexTile nb)
        {
            if (Neighbours.ContainsKey(dir))
            {
                if (!Neighbours[dir].Equals(nb))
                {
                    throw new Exception("Something is wrong");
                }
            }
            else
            {
                Neighbours.Add(dir, nb);
            }
        }
    }
}
