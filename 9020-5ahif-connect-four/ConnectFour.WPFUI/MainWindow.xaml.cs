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
            byte column = (byte)((e.GetPosition((Canvas)sender).X - 20) / 90);
            byte row = 5;
            byte win = 0;

            try
            {
                win = GameBoard.AddStone(column);
            }
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
            }
            catch(ArgumentOutOfRangeException)
            {

            }

            for (; row >= 0; row--)
            {
                if (GameBoard.GameBoard[column, row] != 0)
                {
                    break;
                }
            }

            foreach (Ellipse fe in CanvasBoard.Children)
            {
                if (string.Equals(fe.Name, $"Field{row}{column}"))
                {
                    fe.Fill = ((GameBoard.Turns - 1) % 2) == 0 ? Brushes.Red : Brushes.Yellow;
                    break;
                }
            }

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
