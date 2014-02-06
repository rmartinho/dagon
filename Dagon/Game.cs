using System;
using System.Collections.Generic;
using System.Linq;

namespace Dagon
{
    public class Game
    {
        public Game(Window window)
        {
            var rng = new Random(0);
            State = new GameState
            {
                Dungeon = new Dungeon(window.Width, window.Height),
                Monsters = Enumerable.Range(0, rng.Next(50))
                    .Select(
                        _ =>
                            new Monster
                            {
                                Position = new Point(rng.Next(1, window.Width - 1), rng.Next(1, window.Height - 1))
                            })
                    .ToList(),
            };
            State.Previous = State; // loopback

            Player = new Player {Position = new Point(5, 5)};
            Turns = 0;
        }

        public Player Player { get; set; }
        public int Turns { get; set; }

        public GameState State { get; set; }

        public void Draw(Window window)
        {
            //window.Clear();
            State.Dungeon.Draw(window);
            Player.Draw(window);
            foreach (Monster monster in State.Monsters)
            {
                monster.Draw(window);
            }
        }

        public void Checkpoint()
        {
            State = State.Clone();
        }

        public void Rewind(int n = 1)
        {
            for (int i = 0; i < n; i++)
            {
                State = State.Previous;
            }
        }

        public void MoveMonsters()
        {
            var rng = new Random();
            Checkpoint();
            foreach (Monster monster in State.Monsters)
            {
                int steps = rng.Next(1, 5);
                int direction = rng.Next(0, 4);
                switch (direction)
                {
                    case 0:
                        monster.Position = new Point(monster.Position.X + steps, monster.Position.Y);
                        break;
                    case 1:
                        monster.Position = new Point(monster.Position.X - steps, monster.Position.Y);
                        break;
                    case 2:
                        monster.Position = new Point(monster.Position.X, monster.Position.Y + steps);
                        break;
                    case 3:
                        monster.Position = new Point(monster.Position.X, monster.Position.Y - steps);
                        break;
                }
                // TODO not fall into walls
            }
        }

        public void MovePlayer(int dx, int dy)
        {
            Player.Position = new Point(Player.Position.X + dx, Player.Position.Y + dy);
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