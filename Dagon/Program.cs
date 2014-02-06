namespace Dagon
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var window = new Window();

            var game = new Game(window);

            game.Draw(window);

            // MUHAHAHAHAHA
            for (;;)
            {
            }
        }
    }
}