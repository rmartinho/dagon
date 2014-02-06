namespace Dagon
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var window = new Window();

            var dungeon = new Dungeon(window.Width, window.Height);
            var player = new Player {Position = new Point(15, 15)};

            dungeon.Draw(window);
            player.Draw(window);

            // MUHAHAHAHAHA
            for (;;)
            {
            }
        }
    }
}