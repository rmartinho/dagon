using System;

namespace Dagon
{
    public sealed class Screen
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
            Console.SetCursorPosition(at.X, at.Y);
            Console.Write(c);
        }
    }
}