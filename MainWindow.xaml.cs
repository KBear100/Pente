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
        Player player1 = new Player(Brushes.Red);
        Player player2 = new Player(Brushes.Blue);
        Player currentPlayer;

        public MainWindow()
        {
            InitializeComponent();
            currentPlayer = player1;
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
                if (Check_Win(currentIndex)) 
                {
                    MessageBox.Show("Player 1 Win");
                }
                else currentPlayer = player2;
            }
            else if (currentPlayer == player2)
            {
                (sender as Button).Background = currentPlayer.playerColor;
                (sender as Button).Click -= On_Click;
                if (Check_Win(currentIndex))
                {
                    MessageBox.Show("Player 2 Win");
                }
                else currentPlayer = player1;

            }
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
                if ((Board_Grid.Children[SelectedIndex + i] as Button).Background == currentPlayer.playerColor) count++;
                else break;
            }

            for (int i = 1; count <= 5; i++)
            {
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
                if ((Board_Grid.Children[SelectedIndex + (19 * i)] as Button).Background == currentPlayer.playerColor) count++;
                else break;
            }

            for (int i = 1; count <= 5; i++)
            {
                if ((Board_Grid.Children[SelectedIndex - (19 * i)] as Button).Background == currentPlayer.playerColor) count++;
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
                if ((Board_Grid.Children[SelectedIndex + (19 * i) + i] as Button).Background == currentPlayer.playerColor) count1++;
                else break;
            }

            for (int i = 1; count1 <= 5; i++)
            {
                if ((Board_Grid.Children[SelectedIndex - (19 * i) - i] as Button).Background == currentPlayer.playerColor) count1++;
                else break;
            }

            for (int i = 1; count2 <= 5; i++)
            {
                if ((Board_Grid.Children[SelectedIndex + (19 * i) - i] as Button).Background == currentPlayer.playerColor) count2++;
                else break;
            }

            for (int i = 1; count2 <= 5; i++)
            {
                if ((Board_Grid.Children[SelectedIndex - (19 * i) + i] as Button).Background == currentPlayer.playerColor) count2++;
                else break;
            }

            if (count1 == 3 || count2 == 3) MessageBox.Show("Three");
            if (count2 == 4 || count2 == 4) MessageBox.Show("Four");
            if (count1 == 5 || count2 == 5) return true;

            return false;
        }


    }


}