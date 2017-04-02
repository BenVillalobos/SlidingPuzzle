using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidePuzzle
{
    public enum Direction
    {
        Up = 1,
        Down,
        Left,
        Right
    }

    class Index
    {
        public int Row;
        public int Col;
        public void Set(int r, int c)
        {
            Row = r;
            Col = c;
        }
    }

    class Board
    {
        int[,] _board;
        Random _random = new Random();
        Index emptySpot = new Index();

        public int Width
        {
            get
            {
                return _board.GetLength(0);
            }
        }

        public int Height
        {
            get
            {
                return _board.GetLength(1);
            }
        }

        public Board(int width, int height)
        {
            InitializeBoard(width, height);
        }

        private void InitializeBoard(int width, int height)
        {
            _board = new int[width, height];

            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    if (row == Height - 1 && row == col)
                    {
                        _board[row, col] = -1;
                        continue;
                    }
                    _board[row, col] = (Width * row + col) + 1;
                }
            }
        }
        
        private void SwapOnce(int r1, int c1, int r2, int c2)
        {
            int temp = _board[r1, c1];
            _board[r1, c1] = _board[r2, c2];
            _board[r2, c2] = temp;

            if(_board[r1, c1] == -1)
            {
                emptySpot.Set(r1, c1);
            }
            else if(_board[r2, c2] == -1)
            {
                emptySpot.Set(r2, c2);
            }
        }

        public void SwapRandom(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                SwapOnce(_random.Next(0, Width), _random.Next(0, Height), _random.Next(0, Width), _random.Next(0, Height));
            }
        }

        public bool IsCorrect
        {
            get
            {
                return emptySpot.Row == Width - 1 && emptySpot.Col == Height;
            }
        }

        public void ShowBoard()
        {
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    if(row == emptySpot.Row && col == emptySpot.Col)
                    {
                        Console.Write("   ");
                        continue;
                        
                    }
                    Console.Write(_board[row, col].ToString("## "));
                    if (_board[row, col] < 10)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }

        public void MakeMove(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    if(emptySpot.Row == Height-1)
                    {
                        //can't move
                        return;
                    }
                    SwapOnce(emptySpot.Row, emptySpot.Col, emptySpot.Row + 1, emptySpot.Col);
                    break;
                case Direction.Down:
                    if (emptySpot.Row == 0)
                    {
                        //can't move
                        return;
                    }
                    SwapOnce(emptySpot.Row, emptySpot.Col, emptySpot.Row - 1, emptySpot.Col);
                    break;
                case Direction.Left:
                    if (emptySpot.Col == Width - 1)
                    {
                        //can't move
                        return;
                    }
                    SwapOnce(emptySpot.Row, emptySpot.Col, emptySpot.Row, emptySpot.Col + 1);
                    break;
                case Direction.Right:
                    if (emptySpot.Col == 0)
                    {
                        //can't move
                        return;
                    }
                    SwapOnce(emptySpot.Row, emptySpot.Col, emptySpot.Row, emptySpot.Col - 1);
                    break;
                default:
                    break;
            }
        }
    }
}
