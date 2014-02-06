using System;
using System.Diagnostics;
using System.Threading;

namespace Dagon
{
    internal class Program
    {
        private static void Main()
        {
            var window = new Window();

            var game = new Game(window);

            var sw = Stopwatch.StartNew();
            var lastTick = sw.ElapsedMilliseconds;
            while(true)
            {
                game.Draw(window);
                Thread.Sleep(100);
                if (sw.ElapsedMilliseconds > lastTick + 1000)
                {
                    game.MoveMonsters();
                    lastTick = sw.ElapsedMilliseconds;
                    continue;
                }
                if (Console.KeyAvailable)
                {
                    var k = Console.ReadKey(true);
                    switch (k.KeyChar)
                    {
                        case 'w':
                            game.MovePlayer(0, -1);
                            break;
                        case 'a':
                            game.MovePlayer(-1, 0);
                            break;
                        case 's':
                            game.MovePlayer(0, 1);
                            break;
                        case 'd':
                            game.MovePlayer(1, 0);
                            break;
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                            game.Rewind(k.KeyChar - '0');
                            break;
                    }
                }
            }
        }
    }
}