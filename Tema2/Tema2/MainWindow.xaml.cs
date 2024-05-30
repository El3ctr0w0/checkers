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
using Tema2.Views;

namespace Tema2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //BoardView boardView = new BoardView();
            //boardView.Show();
            //this.Close();
            
        }

        private void StartNewGame_Click(object sender, RoutedEventArgs e)
        {
            bool isChecked = false;
            if (checkBox.IsChecked == true)
                isChecked = true;
            BoardView boardView = new BoardView(isChecked);
            boardView.Show();
            this.Close();
        }

        private void ShowWins_Click(object sender, RoutedEventArgs e)
        {
            Stats stats = new Stats();
            stats.Show();
        }

        private void LoadGame_Click(object sender, RoutedEventArgs e)
        {
            int[,] saveMatrix = Tema2.Commands.ReadCommand.ReadMatrixFromFile("C:\\Csharp\\SaveData.txt");
            int playerTurn = Tema2.Commands.ReadCommand.ReadPlayerTurn("C:\\Csharp\\PlayerTurn.txt");
            bool isChecked = false;
            if (checkBox.IsChecked == true)
                isChecked = true;
            BoardView boardView = new BoardView(isChecked,playerTurn,saveMatrix);
            boardView.Show();
            this.Close();
        }
    }
}