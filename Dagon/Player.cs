using System;

namespace Dagon
{
    public sealed class Player : IDrawable, IPositionable
    {
        public Point Position { get; set; }
        public int Fuel { get; set; }
        public Point OldPosition { get; set; }

        public void Draw(Window window)
        {
            window.Set(Position, '@', ConsoleColor.Green);
        }
    }

    public interface IPositionable
    {
        Point Position { get; }
    }
}