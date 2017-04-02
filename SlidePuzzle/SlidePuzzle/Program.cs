using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidePuzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(3, 3);
            board.SwapRandom(50);
            board.ShowBoard();
            while (!board.IsCorrect)
            {

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.LeftArrow:
                        board.MakeMove(Direction.Left);
                        break;
                    case ConsoleKey.RightArrow:
                        board.MakeMove(Direction.Right);
                        break;
                    case ConsoleKey.UpArrow:
                        board.MakeMove(Direction.Up);
                        break;
                    case ConsoleKey.DownArrow:
                        board.MakeMove(Direction.Down);
                        break;
                    case ConsoleKey.R:
                        board.Reset(3, 3);
                        board.SwapRandom(1);
                        break;
                }
                Console.Clear();
                board.ShowBoard();
            }
            Console.WriteLine("Complete!");
            Console.ReadKey();
        }
    }
}
