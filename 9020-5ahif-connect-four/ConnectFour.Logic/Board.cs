using System;

namespace ConnectFour.Logic
{
    public class Board
    {
        /// <summary>
        /// [Column, Row]
        /// </summary>
        public readonly byte[,] GameBoard = new byte[7, 6];

        public int Turns = 0;

        public bool ended = false;

        public byte AddStone(byte column)
        {
            if (column < 0 || column >= 7)
            {
                throw new ArgumentOutOfRangeException(nameof(column));
            }
            else if (Turns == 42)
            {
                throw new InvalidOperationException("Gameboard is full!");
            }
            else if (ended)
            {
                throw new InvalidOperationException("Game already ended");
            }

            for (byte row = 0; row < 6; row++)
            {
                var cell = GameBoard[column, row];

                if (cell == 0)
                {
                    GameBoard[column, row] = (byte)((Turns % 2) + 1);
                    Turns++;

                    byte end = HasGameEnded(column, row);

                    ended = end != 0;

                    return end;
                }
            }

            throw new InvalidOperationException("Column is full");
        }

        private byte HasGameEnded(byte column, byte row)
        {
            byte win;

            // Check for Vertical Win
            if ((win = CheckForVerticalWin(column, row)) != 0)
            {
                return win;
            }
            // Check for Horizintal Win
            else if ((win = CheckForHorizontalWin(column, row)) != 0)
            {
                return win;
            }
            // Check for diagonal win upper right to lower left
            else if ((win = CheckForDiagonalWin(column, row, 1)) != 0)
            {
                return win;
            }
            // Check for diagonal Win upper left to lower right
            return CheckForDiagonalWin(column, row, -1);
        }

        private byte CheckForVerticalWin(byte column, byte row)
        {
            if (row < 3)
            {
                return 0;
            }

            var player = GameBoard[column, row];
            for (var i = 0; i < 3; i++)
            {
                if (GameBoard[column, row - i - 1] != player)
                {
                    return 0;
                }
            }

            return player;
        }

        private byte CheckForHorizontalWin(byte column, byte row)
        {
            byte player = GameBoard[column, row];
            byte count = 0;

            for (var i = 6; i >= 3; i--)
            {
                if (GameBoard[i, row] != player)
                {
                    continue;
                }

                for (int j = i; j >= i - 3; j--)
                {
                    if (GameBoard[j, row] != player)
                    {
                        count = 0;
                        break;
                    }

                    count++;
                }

                if (count == 4)
                {
                    break;
                }
            }

            return count == 4 ? player : (byte)0;
        }

        private byte CheckForDiagonalWin(byte column, byte row, int direction)
        {
            byte player = GameBoard[column, row];
            byte count = 0;

            while (true)
            {
                if (column >= 7 || row >= 6)
                {
                    break;
                }
                else if (GameBoard[column, row] != player)
                {
                    break;
                }

                column--;
                row -= (byte)(1 * direction);
            }

            column++;
            row += (byte)(1 * direction);

            while (true)
            {
                if (column >= 7 || row >= 6)
                {
                    break;
                }
                else if (count == 4)
                {
                    break;
                }
                else if (GameBoard[column, row] != player)
                {
                    break;
                }

                column++;
                row += (byte)(1 * direction);
                count++;
            }

            return count == 4 ? player : (byte)0;
        }

        public void PrintBoard()
        {
            for (int i = 5; i >= 0; i--)
            {
                for (int j = 0; j < 7; j++)
                {
                    Console.Write(GameBoard[j, i] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
