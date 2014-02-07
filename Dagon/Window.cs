using System;

namespace Dagon
{
    public sealed class Window
    {
        public int Width
        {
            get { return Console.WindowWidth; }
        }

        public int Height
        {
            get { return Console.WindowHeight-1; }
        }

        public void Set(Point at, char c, ConsoleColor colour)
        {
            if (at.X < 0 || at.X >= Width || at.Y < 0 || at.Y >= Height) return;
            Console.ForegroundColor = colour;
            Console.SetCursorPosition(at.X, at.Y);
            Console.Write(c);
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}