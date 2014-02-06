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
            get { return Console.WindowHeight; }
        }

        public void Set(Point at, char c)
        {
            if (at.X < 0 || at.X >= Width || at.Y < 0 || at.Y >= Height) return;
            Console.SetCursorPosition(at.X, at.Y);
            Console.Write(c);
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}