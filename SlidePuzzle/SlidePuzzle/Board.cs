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
        Index _emptySpot = new Index();

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

        public void Reset(int width, int height)
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
            _emptySpot.Set(Width - 1, Height - 1);
        }
        
        private void SwapOnce(int r1, int c1, int r2, int c2)
        {
            int temp = _board[r1, c1];
            _board[r1, c1] = _board[r2, c2];
            _board[r2, c2] = temp;

            if(_board[r1, c1] == -1)
            {
                _emptySpot.Set(r1, c1);
            }
            else if(_board[r2, c2] == -1)
            {
                _emptySpot.Set(r2, c2);
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
                int lastNum = int.MinValue;
                //if the empty spot isn't at the bottom right corner, we automatically know it isn't solved
                if(_emptySpot.Row != Width -1 || _emptySpot.Col != Height-1)
                {
                    return false;
                }
                //count through each index. Must be in ascending order until the end
                for (int row = 0; row < Height; row++)
                {
                                                     //bottom right corner check
                    for (int col = 0; col < Width && !(row == Width - 1 && col == row); col++)
                    {
                        if(_board[row, col] < lastNum)
                        {
                            return false;
                        }
                        lastNum = _board[row, col];
                    }
                }
                return true;
            }
        }

        public void ShowBoard()
        {
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    if(row == _emptySpot.Row && col == _emptySpot.Col)
                    {
                        Console.Write("|    ");
                        if (col == Width - 1)
                        {
                            Console.Write("|");
                        }
                        continue;
                    }

                    Console.Write(string.Format("| {0} ", _board[row, col].ToString("##")));

                    if (_board[row, col] < 10)
                    {
                        Console.Write(" ");
                    }
                    if (col == Width - 1)
                    {
                        Console.Write("|");
                    }
                }
                Console.WriteLine();
            }
        }

        public void MakeMove(Direction dir)
        {
            switch (dir)
            {
                case Direction.Down:
                    if(_emptySpot.Row == Height-1)
                    {
                        return;
                    }
                    SwapOnce(_emptySpot.Row, _emptySpot.Col, _emptySpot.Row + 1, _emptySpot.Col);
                    break;
                case Direction.Up:
                    if (_emptySpot.Row == 0)
                    {
                        //can't move
                        return;
                    }
                    SwapOnce(_emptySpot.Row, _emptySpot.Col, _emptySpot.Row - 1, _emptySpot.Col);
                    break;
                case Direction.Right:
                    if (_emptySpot.Col == Width - 1)
                    {
                        //can't move
                        return;
                    }
                    SwapOnce(_emptySpot.Row, _emptySpot.Col, _emptySpot.Row, _emptySpot.Col + 1);
                    break;
                case Direction.Left:
                    if (_emptySpot.Col == 0)
                    {
                        //can't move
                        return;
                    }
                    SwapOnce(_emptySpot.Row, _emptySpot.Col, _emptySpot.Row, _emptySpot.Col - 1);
                    break;
                default:
                    break;
            }
        }
    }
}
