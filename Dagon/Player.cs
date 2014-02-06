namespace Dagon
{
    public sealed class Player : IDrawable
    {
        public Point Position { get; set; }

        public void Draw(Window window)
        {
            window.Set(Position, '@');
        }
    }
}