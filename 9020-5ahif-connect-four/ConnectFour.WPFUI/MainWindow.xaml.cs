using ConnectFour.Logic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ConnectFour.WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Board GameBoard;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            GameBoard = new Board();
        }

        private void CanvasBoard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // starting Variables
            byte column = (byte)((e.GetPosition((Canvas)sender).X - 20) / 90);
            byte row = 5;
            byte win = 0;

            // Add Stone on Gameboard in column
            try
            {
                win = GameBoard.AddStone(column);
            }
            // Show Exceptions in MessageBox
            catch (InvalidOperationException ex)
            {
                switch (ex.Message)
                {
                    case "Gameboard is full!":
                        MessageBox.Show("Das Spielbrett ist schon voll befüllt!");
                        break;
                    case "Game already ended":
                        MessageBox.Show("Das Spiel wurde schon beendet!");
                        break;
                    case "Column is full":
                        MessageBox.Show($"Die Spalte {column} ist schon befüllt!");
                        break;
                }
                return;
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }

            // This is a risky operation, as byte is an unsigned value and is always above or zero, 
            // but because Adding a Stone promises that it will be added or throws an exception.
            // But the thown Exceptions get catched and after that, they exit the method, 
            // therefore this for-loop works.
            for (; row >= 0; row--)
            {
                if (GameBoard.GameBoard[column, row] != 0)
                {
                    break;
                }
            }

            // Searches the correct Elllipse and changes the color
            foreach (Ellipse fe in CanvasBoard.Children)
            {
                if (string.Equals(fe.Name, $"Field{row}{column}"))
                {
                    fe.Fill = ((GameBoard.Turns - 1) % 2) == 0 ? Brushes.Yellow : Brushes.Red;
                    break;
                }
            }

            // Shows a MessageBox if somebody won or is in a tie
            if (win != 0)
            {
                MessageBox.Show($"Spieler {((GameBoard.Turns - 1) % 2) + 1} hat gewonnen!");
            }
            else if (GameBoard.Turns == 42)
            {
                MessageBox.Show("Das Spielbrett ist voll befüllt! Es ist ein Unentschieden! Keiner hat gewonnen!");
            }
        }
    }
}
