using System.Collections.Generic;
using System.Linq;

namespace Dagon
{
    public class Game
    {
        public Game(Window window)
        {
            State = new GameState
            {
                Dungeon = new Dungeon(window.Width, window.Height),
                Monsters = new List<Monster>(),
            };
            State.Previous = State; // loopback
        }

        public Player Player { get; set; }

        public GameState State { get; set; }

        private void StartAction()
        {
            State = State.Clone();
        }
    }

    public class GameState
    {
        public Dungeon Dungeon { get; set; }
        public IList<Monster> Monsters { get; set; }

        public GameState Previous { get; set; }

        public GameState Clone()
        {
            return new GameState
            {
                Dungeon = Dungeon.Clone(),
                Monsters = Monsters.Select(m => m.Clone()).ToList(),
                Previous = this,
            };
        }
    }

    public class Monster : IDrawable
    {
        public Point Position { get; set; }

        public void Draw(Window window)
        {
            window.Set(Position, 'X');
        }

        public Monster Clone()
        {
            return new Monster {Position = Position};
        }
    }
}