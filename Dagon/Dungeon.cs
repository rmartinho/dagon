using System;
using System.Collections.Generic;

namespace Dagon
{
    public enum TileKind
    {
        Wall,
        Open,
    }

    public struct Tile
    {
        public TileKind Kind { get; set; }

        public static implicit operator char(Tile tile)
        {
            return tile.Kind == TileKind.Wall ? '#' : '·';
        }
    }

    public sealed class Dungeon
    {
        private readonly Tile[,] _tiles;

        public Dungeon(int width, int height)
        {
            _tiles = new Tile[width, height];

            var rooms = new List<Tuple<Point, Point>>();

            var rng = new Random(2);
            int nRooms = rng.Next(10, 30);
            for (int i = 0; i < nRooms; i++)
            {
                int roomWidth = rng.Next(3, 6);
                int roomHeight = rng.Next(3, 6);
                int top = rng.Next(1, height - roomHeight);
                int left = rng.Next(1, width - roomWidth);

                var room = Tuple.Create(new Point(left, top), new Point(left + roomWidth, top + roomHeight));
                rooms.Add(room);
                CarveRoom(room.Item1, room.Item2);
            }
        }

        public Tile this[int width, int height]
        {
            get { return _tiles[width, height]; }
        }

        public Tile this[Point point]
        {
            get { return _tiles[point.X, point.Y]; }
        }

        public int Width
        {
            get { return _tiles.GetLength(0); }
        }

        public int Height
        {
            get { return _tiles.GetLength(1); }
        }

        public void Draw(Window window)
        {
            for (int i = 0; i < Width; ++i)
            {
                for (int j = 0; j < Height; j++)
                {
                    window.Set(new Point(i, j), _tiles[i, j]);
                }
            }
        }

        private void CarveRoom(Point topLeft, Point bottomRight)
        {
            for (int i = topLeft.X; i < bottomRight.X; ++i)
            {
                for (int j = topLeft.Y; j < bottomRight.Y; j++)
                {
                    _tiles[i, j] = new Tile {Kind = TileKind.Open};
                }
            }
        }
    }

    public struct Point
    {
        public Point(int x, int y) : this()
        {
            X = x;
            Y = y;
        }

        public int X { get; private set; }
        public int Y { get; private set; }
    }
}