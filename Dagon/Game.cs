﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Dagon
{
    public class Game
    {
        private readonly Window _window;

        public Game(Window window)
        {
            _window = window;
            var rng = new Random();
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

            Player = new Player {Position = new Point(5, 5), Fuel = 10};
            Turns = 0;
        }

        public Player Player { get; set; }
        public int Turns { get; set; }

        public GameState State { get; set; }

        public void Draw(Window window)
        {
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
            _window.Clear();
            int moves = Math.Min(n, Player.Fuel);
            for (int i = 0; i < moves; i++)
            {
                State = State.Previous;
            }
            Player.Fuel -= moves;
        }

        public void MoveMonsters()
        {
            Checkpoint();
            foreach (Monster monster in State.Monsters)
            {
                int dx = Player.Position.X - monster.Position.X;
                int dy = Player.Position.Y - monster.Position.Y;

                _window.Set(monster.Position, ' ', ConsoleColor.White);
                monster.Position = new Point(monster.Position.X + Math.Sign(dx), monster.Position.Y + Math.Sign(dy));
            }

            CheckMonsterStates();
        }

        private void CheckMonsterStates()
        {
            IEnumerable<Monster> deadMonsters = (from m1 in State.Monsters
                from m2 in State.Monsters
                where m1 != m2 && m1.Position.X == m2.Position.X && m1.Position.Y == m2.Position.Y
                select m1).ToList();

            foreach (Monster monster in deadMonsters)
            {
                State.Monsters.Remove(monster);
            }
            if (State.Monsters.Any(m => m.Position.X == Player.Position.X && m.Position.Y == Player.Position.Y))
            {
                throw new YouLost();
            }
        }

        public void MovePlayer(int dx, int dy)
        {
            _window.Set(Player.Position, ' ', ConsoleColor.White);
            Player.Position = new Point(Player.Position.X + dx, Player.Position.Y + dy);
        }
    }

    internal class YouLost : Exception
    {
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

    public class Monster : IDrawable, IPositionable
    {
        public Point Position { get; set; }

        public void Draw(Window window)
        {
            window.Set(Position, 'X', ConsoleColor.Red);
        }

        public Monster Clone()
        {
            return new Monster {Position = Position};
        }
    }
}