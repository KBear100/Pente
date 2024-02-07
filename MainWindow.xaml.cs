using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pente
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int currentIndex = 0;
        int x = 0;
        int y = 0;

        int player = 1;
        Player player1 = new Player(Brushes.Red, Board.player1Name);
        Player player2 = new Player(Brushes.Blue, Board.player2Name);
        Player currentPlayer;
        Player enemyPlayer;

        public MainWindow()
        {
            InitializeComponent();
            currentPlayer = player1;
            enemyPlayer = player2;
            for (int i = 0; i < Board.boardSize; i++)
            {
                for (int j = 0; j < Board.boardSize; j++)
                {
                    Button button = new Button();
                    Thickness margin = new Thickness();
                    margin.Left = 37 * j;
                    margin.Right = 37 * j;
                    margin.Top = 37 * i;
                    margin.Bottom = 37 * i;

                    button.Click += On_Click;
                    button.Background = new SolidColorBrush(Colors.Transparent);
                    button.Width = 30;
                    button.Height = 30;
                    button.HorizontalAlignment = HorizontalAlignment.Left;
                    button.VerticalAlignment = VerticalAlignment.Top;
                    button.Margin = margin;
                    Board_Grid.Children.Add(button);
                }
            }
        }
        public void On_Click(object sender, RoutedEventArgs e)
        {
            currentIndex = Board_Grid.Children.IndexOf(sender as Button);

            int x = currentIndex % 19 + 1;
            int y = currentIndex / 19 + 1;

            if (currentPlayer == player1)
            {
                (sender as Button).Background = currentPlayer.playerColor;
                (sender as Button).Click -= On_Click;
                CheckCaptureHorizontal(currentIndex);
                CheckCaptureVertical(currentIndex);
                CheckCaptureDiagonal(currentIndex);
                if (Check_Win(currentIndex))
                {
                    MessageBox.Show(player1.playerName + " Wins");
                    Window title = new StartScreen();
                    title.Show();
                    this.Close();
                }
                else
                {
                    currentPlayer = player2;
                    enemyPlayer = player1;
                }
                if (Board.numPlayers == 1) ComputerMove();
            }
            else if (currentPlayer == player2)
            {
                (sender as Button).Background = currentPlayer.playerColor;
                (sender as Button).Click -= On_Click;
                CheckCaptureHorizontal(currentIndex);
                CheckCaptureVertical(currentIndex);
                CheckCaptureDiagonal(currentIndex);
                if (Check_Win(currentIndex))
                {
                    MessageBox.Show(player2.playerName + " Wins");
                    Window title = new StartScreen();
                    title.Show();
                    this.Close();
                }
                else
                {
                    currentPlayer = player1;
                    enemyPlayer = player2;
                }
            }
        }

        public void ComputerMove()
        {
            Random random = new Random();
            currentIndex = random.Next(0, Board_Grid.Children.Count + 1);
            Button button = (Button)Board_Grid.Children[currentIndex];

            while (button.Background == Brushes.Red || button.Background == Brushes.Blue)
            {
                currentIndex = random.Next(0, Board_Grid.Children.Count + 1);
                button = (Button)Board_Grid.Children[currentIndex];
            }

            button.Background = currentPlayer.playerColor;
            button.Click -= On_Click;
            if (Check_Win(currentIndex))
            {
                MessageBox.Show(player2.playerName + " Wins");
                Window title = new StartScreen();
                title.Show();
                this.Close();
            }

            currentPlayer = player1;
            enemyPlayer = player2;
        }

        public bool Check_Win(int SelectedIndex)
        {
            if (Check_Horizontal(SelectedIndex) || (Check_Vertical(SelectedIndex)) || Check_Diagonals(SelectedIndex))
            {
                return true;
            }
            return false;
        }

        public bool Check_Horizontal(int SelectedIndex)
        {
            int count = 1;

            for (int i = 1; count <= 5; i++)
            {
                if (SelectedIndex + i >= (Board.boardSize * Board.boardSize)) break;
                else if ((Board_Grid.Children[SelectedIndex + i] as Button).Background == currentPlayer.playerColor) count++;
                else break;
            }

            for (int i = 1; count <= 5; i++)
            {
                if (SelectedIndex - i < 0) break;
                if ((Board_Grid.Children[SelectedIndex - i] as Button).Background == currentPlayer.playerColor) count++;
                else break;
            }

            if (count == 3) MessageBox.Show("Three");
            if (count == 4) MessageBox.Show("Four");
            if (count == 5) return true;
            return false;
        }

        public bool Check_Vertical(int SelectedIndex)
        {
            int count = 1;

            for (int i = 1; count <= 5; i++)
            {
                if (SelectedIndex + (Board.boardSize * i) >= Board.boardSize * Board.boardSize) break;
                else if ((Board_Grid.Children[SelectedIndex + (Board.boardSize * i)] as Button).Background == currentPlayer.playerColor) count++;
                else break;
            }

            for (int i = 1; count <= 5; i++)
            {
                if (SelectedIndex - (Board.boardSize * i) < 0) break;
                else if ((Board_Grid.Children[SelectedIndex - (Board.boardSize * i)] as Button).Background == currentPlayer.playerColor) count++;
                else break;
            }

            if (count == 5) return true;

            if (count == 3) MessageBox.Show("Three");
            if (count == 4) MessageBox.Show("Four");
            return false;
        }

        public bool Check_Diagonals(int SelectedIndex)
        {
            int count1 = 1;
            int count2 = 1;

            for (int i = 1; count1 <= 5; i++)
            {
                if ((SelectedIndex + (Board.boardSize * i) + i) >= (Board.boardSize * Board.boardSize)) break;
                else if ((Board_Grid.Children[SelectedIndex + (Board.boardSize * i) + i] as Button).Background == currentPlayer.playerColor) count1++;
                else break;
            }

            for (int i = 1; count1 <= 5; i++)
            {
                if ((SelectedIndex - (Board.boardSize * i) - i) < 0) break;
                else if ((Board_Grid.Children[SelectedIndex - (Board.boardSize * i) - i] as Button).Background == currentPlayer.playerColor) count1++;
                else break;
            }
            
            for (int i = 1; count2 <= 5; i++)
            {
                if ((SelectedIndex + (Board.boardSize * i) - i) >= (Board.boardSize * Board.boardSize)) break;
                else if ((Board_Grid.Children[SelectedIndex + (Board.boardSize * i) - i] as Button).Background == currentPlayer.playerColor) count2++;
                else break;
            }

            for (int i = 1; count2 <= 5; i++)
            {
                if ((SelectedIndex - (Board.boardSize * i) + i) < 0) break;
                else if ((Board_Grid.Children[SelectedIndex - (Board.boardSize * i) + i] as Button).Background == currentPlayer.playerColor) count2++;
                else break;
            }

            if (count1 == 3 || count2 == 3) MessageBox.Show("Three");
            if (count2 == 4 || count2 == 4) MessageBox.Show("Four");
            if (count1 == 5 || count2 == 5) return true;

            return false;
        }

        public void CheckCaptureHorizontal(int SelectedIndex)
        {
            int count = 0;

            for (int i = 1; count <= 2; i++)
            {
                if (SelectedIndex + i >= (Board.boardSize * Board.boardSize)) break;
                else if ((Board_Grid.Children[SelectedIndex + i] as Button).Background == enemyPlayer.playerColor)
                {
                    count++;
                    if (count == 2 && (Board_Grid.Children[SelectedIndex + 3] as Button).Background == currentPlayer.playerColor)
                    {
                        (Board_Grid.Children[SelectedIndex + 1] as Button).Background = Brushes.Transparent;
                        (Board_Grid.Children[SelectedIndex + 1] as Button).Click += On_Click;
                        (Board_Grid.Children[SelectedIndex + 2] as Button).Background = Brushes.Transparent;
                        (Board_Grid.Children[SelectedIndex + 2] as Button).Click += On_Click;

                        break;
                    }
                }
                else break;
            }

            for (int i = 1; count <= 2; i++)
            {
                if (SelectedIndex - i < 0) break;
                else if ((Board_Grid.Children[SelectedIndex - i] as Button).Background == enemyPlayer.playerColor)
                {
                    count++;
                    if (count == 2 && (Board_Grid.Children[SelectedIndex - 3] as Button).Background == currentPlayer.playerColor)
                    {
                        (Board_Grid.Children[SelectedIndex - 1] as Button).Background = Brushes.Transparent;
                        (Board_Grid.Children[SelectedIndex - 1] as Button).Click += On_Click;
                        (Board_Grid.Children[SelectedIndex - 2] as Button).Background = Brushes.Transparent;
                        (Board_Grid.Children[SelectedIndex - 2] as Button).Click += On_Click;
                        break;
                    }
                }
                else break;
            }
        }

        public void CheckCaptureVertical(int SelectedIndex)
        {
            int count = 0;

            for (int i = 1; count <= 2; i++)
            {
                if (SelectedIndex + (Board.boardSize * i) >= Board.boardSize * Board.boardSize) break;
                else if ((Board_Grid.Children[SelectedIndex + (Board.boardSize * i)] as Button).Background == enemyPlayer.playerColor)
                {
                    count++;
                    if (count == 2 && (Board_Grid.Children[SelectedIndex + (Board.boardSize * 3)] as Button).Background == currentPlayer.playerColor)
                    {
                        (Board_Grid.Children[SelectedIndex + (Board.boardSize)] as Button).Background = Brushes.Transparent;
                        (Board_Grid.Children[SelectedIndex + (Board.boardSize)] as Button).Click += On_Click;
                        (Board_Grid.Children[SelectedIndex + (Board.boardSize * 2)] as Button).Background = Brushes.Transparent;
                        (Board_Grid.Children[SelectedIndex + (Board.boardSize * 2)] as Button).Click += On_Click;
                        break;
                    }
                }
                else break;
            }

            for (int i = 1; count <= 2; i++)
            {
                if (SelectedIndex - (Board.boardSize * i) < 0) break;
                else if ((Board_Grid.Children[SelectedIndex - (Board.boardSize * i)] as Button).Background == enemyPlayer.playerColor)
                {
                    count++;
                    if (count == 2 && (Board_Grid.Children[SelectedIndex - (Board.boardSize * 3)] as Button).Background == currentPlayer.playerColor)
                    {
                        (Board_Grid.Children[SelectedIndex - (Board.boardSize)] as Button).Background = Brushes.Transparent;
                        (Board_Grid.Children[SelectedIndex - (Board.boardSize)] as Button).Click += On_Click;
                        (Board_Grid.Children[SelectedIndex - (Board.boardSize)] as Button).Background = Brushes.Transparent;
                        (Board_Grid.Children[SelectedIndex - (Board.boardSize)] as Button).Click += On_Click;
                        break;
                    }
                }
                else break;
            }


        }
        public void CheckCaptureDiagonal(int SelectedIndex)
        {
            int count1 = 0;
            int count2 = 0;

            for (int i = 1; count1 <= 2; i++)
            {
                if ((SelectedIndex + (Board.boardSize * i) + i) >= (Board.boardSize * Board.boardSize)) break;
                else if ((Board_Grid.Children[SelectedIndex + (Board.boardSize * i) + i] as Button).Background == enemyPlayer.playerColor)
                {
                    count1++;
                    if (count1 == 2 && (Board_Grid.Children[SelectedIndex + (Board.boardSize * 3) + 3] as Button).Background == currentPlayer.playerColor)
                    {
                        (Board_Grid.Children[SelectedIndex + (Board.boardSize) + 1] as Button).Background = Brushes.Transparent;
                        (Board_Grid.Children[SelectedIndex + (Board.boardSize) + 1] as Button).Click += On_Click;
                        (Board_Grid.Children[SelectedIndex + (Board.boardSize * 2) + 2] as Button).Background = Brushes.Transparent;
                        (Board_Grid.Children[SelectedIndex + (Board.boardSize * 2) + 2] as Button).Click += On_Click;

                        break;
                    }
                }
                else break;
            }

            for (int i = 1; count1 <= 2; i++)
            {
                if ((SelectedIndex - (Board.boardSize * i) - i) < 0) break;
                else if ((Board_Grid.Children[SelectedIndex - (Board.boardSize * i) - i] as Button).Background == enemyPlayer.playerColor)
                {
                    count1++;
                    if (count1 == 2 && (Board_Grid.Children[SelectedIndex - (Board.boardSize * 3) - 3] as Button).Background == currentPlayer.playerColor)
                    {
                        (Board_Grid.Children[SelectedIndex - (Board.boardSize) - 1] as Button).Background = Brushes.Transparent;
                        (Board_Grid.Children[SelectedIndex - (Board.boardSize) - 1] as Button).Click += On_Click;
                        (Board_Grid.Children[SelectedIndex - (Board.boardSize * 2) - 2] as Button).Background = Brushes.Transparent;
                        (Board_Grid.Children[SelectedIndex - (Board.boardSize * 2) - 2] as Button).Click += On_Click;

                        break;
                    }
                }
                else break;
            }

            for (int i = 1; count2 <= 2; i++)
            {
                if ((SelectedIndex + (Board.boardSize * i) - i) >= (Board.boardSize * Board.boardSize)) break;
                else if ((Board_Grid.Children[SelectedIndex + (Board.boardSize * i) - i] as Button).Background == enemyPlayer.playerColor)
                {
                    count2++;
                    if (count2 == 2 && (Board_Grid.Children[SelectedIndex + (Board.boardSize * 3) - 3] as Button).Background == currentPlayer.playerColor)
                    {
                        (Board_Grid.Children[SelectedIndex + (Board.boardSize) - 1] as Button).Background = Brushes.Transparent;
                        (Board_Grid.Children[SelectedIndex + (Board.boardSize) - 1] as Button).Click += On_Click;
                        (Board_Grid.Children[SelectedIndex + (Board.boardSize * 2) - 2] as Button).Background = Brushes.Transparent;
                        (Board_Grid.Children[SelectedIndex + (Board.boardSize * 2) - 2] as Button).Click += On_Click;

                        break;
                    }
                }
                else break;
            }

            for (int i = 1; count2 <= 2; i++)
            {
                if ((SelectedIndex - (Board.boardSize * i) + i) < 0) break;
                else if ((Board_Grid.Children[SelectedIndex - (Board.boardSize * i) + i] as Button).Background == enemyPlayer.playerColor)
                {
                    count2++;
                    if (count2 == 2 && (Board_Grid.Children[SelectedIndex - (Board.boardSize * 3) + 3] as Button).Background == currentPlayer.playerColor)
                    {
                        (Board_Grid.Children[SelectedIndex - (Board.boardSize) + 1] as Button).Background = Brushes.Transparent;
                        (Board_Grid.Children[SelectedIndex - (Board.boardSize) + 1] as Button).Click += On_Click;
                        (Board_Grid.Children[SelectedIndex - (Board.boardSize * 2) + 2] as Button).Background = Brushes.Transparent;
                        (Board_Grid.Children[SelectedIndex - (Board.boardSize * 2) + 2] as Button).Click += On_Click;

                        break;
                    }
                }
                else break;
            }
        }

    }


}