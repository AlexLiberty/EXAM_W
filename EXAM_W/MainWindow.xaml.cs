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
using System.Windows.Threading;

namespace EXAM_W
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game gameWindow;
        public MainWindow()
        {
            InitializeComponent();
        } 
        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            if (gameWindow == null || !gameWindow.IsVisible)
            {
                gameWindow = new Game();
                gameWindow.Show();
            }
            Close();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        internal void NewGame_Click(object value1, object value2)
        {
            if (gameWindow == null || !gameWindow.IsVisible)
            {
                gameWindow = new Game();
                gameWindow.Show();
            }
            Close();
        }
    }
}
