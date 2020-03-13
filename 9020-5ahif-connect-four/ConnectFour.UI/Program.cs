using ConnectFour.Logic;
using System;

namespace ConnectFour.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            byte win = 0;

            // Logik des Spiels
            while(!board.ended)
            {
                // Ausgeben des Spielbretts
                Console.WriteLine("Spielbrett:");
                Console.WriteLine();
                board.PrintBoard();
                Console.WriteLine();

                // Einlesen der Zahl
                Console.WriteLine($"Spieler {(board.Turns % 2) + 1}: Bitte geben sie eine Spaltenzahl ein!");
                string input;
                while (string.IsNullOrEmpty(input = Console.ReadLine().Trim()))
                {
                    Console.WriteLine();
                    Console.Error.WriteLine($"Spieler {(board.Turns % 2) + 1}: Sie haben keine Spaltenzahl eingegeben!");
                    Console.WriteLine();
                    Console.WriteLine($"Spieler {(board.Turns % 2) + 1}: Bitte geben sie eine Spaltenzahl ein!");
                }
                Console.WriteLine();

                // Zahl Parsen
                byte column;
                if (!Byte.TryParse(input, out column))
                {
                    Console.Error.WriteLine($"Spieler {(board.Turns % 2) + 1}: Ihre Eingabe ist keine Zahl!");
                    Console.WriteLine();
                    continue;
                }

                // Spieler fügt Stein in Spalte zu
                try
                {
                    win = board.AddStone(column);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.Error.WriteLine("Eine Spaltenzahl außerhalb der Reichweite wurde eingegeben!");
                    Console.WriteLine();
                    continue;
                }
                catch(InvalidOperationException e)
                {
                    switch(e.Message)
                    {
                        case "Gameboard is full!":
                            Console.Error.WriteLine("Das Spielbrett ist schon voll befüllt!");
                            break;
                        case "Game already ended":
                            Console.Error.WriteLine("Das Spiel wurde schon beendet!");
                            break;
                        case "Column is full":
                            Console.Error.WriteLine($"Die Spalte {column} ist schon befüllt!");
                            break;
                    }
                    Console.WriteLine();
                    continue;
                }
            }

            // Letztes ausgeben des Spielbretts
            board.PrintBoard();
            Console.WriteLine();

            // Ausgeben des Gewinners
            if (win == 0)
            {
                Console.WriteLine("Das Spielbrett ist voll befüllt! Keiner hat gewonnen!");
            }
            else
            {
                Console.WriteLine($"Spieler {((board.Turns - 1) % 2) + 1} hat gewonnen!");
            }
        }
    }
}
