using ClientSide;
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

namespace Views
{
    /// <summary>
    /// Interaction logic for Multi_Player.xaml
    /// </summary>
    public partial class Multi_Player : Window
    {
        private StartGameViewModel sgvm;
        private GameViewModel gvm;
        private GUIClient c;
        public Multi_Player(StartGameViewModel sgvm)
        {
            InitializeComponent();
            this.sgvm = sgvm;
            DataContext = sgvm;
            this.gvm = new GameViewModel(new ApplicationGameModel());
            this.c = new GUIClient(gvm);
            string gamesList = c.ListGames();
            List<string> games = parseGames(gamesList);
            foreach (string game in games)
            {
                this.games.Items.Add(game);
            }
        }

        private void changeWindow(Window w)
        {
            Application.Current.MainWindow = w;
            this.Close();
            w.Show();
        }

        private List<string> parseGames(string gameList)
        {
            gameList = gameList.Substring(1);
            gameList = gameList.Remove(gameList.Length - 1);
            string[] gamesWithoutN = gameList.Split(',');
            List<string> games = new List<string>();
            foreach (string game in gamesWithoutN)
            {
                string gameWithoutN = game.Substring(1);
                games.Add(gameWithoutN);
            }
            games.Remove("");
            return games;
        }

        public void join_Click(Object SENDER, RoutedEventArgs e)
        {
            string gameToJoin = this.games.SelectedItem.ToString();
            if (gameToJoin != null)
            {
                string game = c.JoinGame(gameToJoin);
                StartPlaying(game);
            }
                
        }

        private void StartPlaying (string mazeString)
        {
            MultiPlayerGameModel mpgm = new MultiPlayerGameModel();
            MultiPlayerGameViewModel mpgvm = new MultiPlayerGameViewModel(mpgm);
            Maze m = Maze.FromJSON(mazeString);
            mpgvm.MazeObject = m;
            mpgvm.PlayerImageFile = "SuperMario.png";
            mpgvm.ExitImageFile = "Castle.png";
            MultiPlayerGame mpg = new MultiPlayerGame(mpgvm, c);
            changeWindow(mpg);
        }

        public void btnStart_Click(Object SENDER, RoutedEventArgs e)
        {
            string mazeString = c.StartGame(sgvm.MazeCols, sgvm.MazeRows, sgvm.MazeName);
            StartPlaying(mazeString);

        }
    }
}
