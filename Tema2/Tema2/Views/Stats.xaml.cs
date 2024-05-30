using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tema2
{
    /// <summary>
    /// Interaction logic for Stats.xaml
    /// </summary>
    public partial class Stats : Window
    {
        public Stats()
        {
            InitializeComponent();
            Tuple<int, int> tuple = Tema2.Commands.ReadCommand.ReadStats("C:\\Csharp\\Statistics.txt");
            RedWins.Text = tuple.Item1.ToString();
            MaroonWins.Text = tuple.Item2.ToString();
        }
    }
}
