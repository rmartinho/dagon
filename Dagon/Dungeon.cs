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
            return tile.Kind == TileKind.Wall ? '#' : '.';
        }
    }

    public sealed class Dungeon
    {
        private readonly Tile[,] _tiles;

        public Dungeon(int width, int height)
        {
            _tiles = new Tile[width, height];
            CarveRoom(new Point(10, 10), new Point(20, 20));
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

        public void Draw(Screen screen)
        {
            for (int i = 0; i < Width; ++i)
            {
                for (int j = 0; j < Height; j++)
                {
                    screen.Set(new Point(i, j), _tiles[i, j]);
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