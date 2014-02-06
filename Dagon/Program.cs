namespace Dagon
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var screen = new Screen();
            var dungeon = new Dungeon(screen.Width, screen.Height);
            dungeon.Draw(screen);
        }
    }
}