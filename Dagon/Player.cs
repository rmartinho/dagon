﻿namespace Dagon
{
    public sealed class Player
    {
        public int Health { get; set; }
        public Point Position { get; set; }

        public void Draw(Window window)
        {
            window.Set(Position, '@');
        }
    }
}