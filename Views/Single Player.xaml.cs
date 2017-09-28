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
using ViewModels;
using Models;
using MazeLib;
using ClientSide;
namespace Views
{
    /// <summary>
    /// Interaction logic for Single_Player.xaml
    /// </summary>
    public partial class Single_Player : Window
    {
        private StartGameViewModel sgvm;
        public Single_Player(StartGameViewModel sgvm)
        {
            InitializeComponent();
            this.sgvm = sgvm;
            DataContext = sgvm;

        }

        public void btnStart_Click(Object SENDER, RoutedEventArgs e)
        {

            ApplicationGameModel agm = new ApplicationGameModel();
            GameViewModel gvm = new GameViewModel(agm);
            SinglePlayerGame w = new SinglePlayerGame(gvm, sgvm.MazeCols, sgvm.MazeRows, sgvm.MazeName);

            Application.Current.MainWindow = w;
            this.Close();
            w.Show();
        }

    }
}
