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
        int yes = 0;
        int x = 0;
        int y = 0;

        public MainWindow()
        {
            InitializeComponent();
        }
        public void On_Click(object sender, RoutedEventArgs e)
        {
            yes = Board_Grid.Children.IndexOf(sender as Button);

            int x = yes % 19;
            int y = yes / 19;

            MessageBox.Show(x + "," + y);
        }
    }


}