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
            Board board = new Board(5, 5);
            board.SwapRandom(50);
            
            while(!board.IsCorrect)
            {
                board.ShowBoard();
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
                }
                Console.Clear();
            }
        }
    }
}
