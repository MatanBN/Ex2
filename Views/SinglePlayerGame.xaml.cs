using ClientSide;
using MazeLib;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using ViewModels;

namespace Views
{

    
    /// <summary>
    /// Interaction logic for SinglePlayerGame.xaml
    /// </summary>
    public partial class SinglePlayerGame : Window
    {
        private GameViewModel gvm;
        private GUIClient c;
        private int counter = 0;
        private string stringSolution;
        private double xPlace;
        private double yPlace;
        private Image copyPlayer;
        public SinglePlayerGame(GameViewModel gvm, int cols, int rows, string mazeName)
        {

            c = new GUIClient(gvm);
            c.GenerateMaze(cols, rows, mazeName);
            this.gvm = gvm;
            this.DataContext = gvm;
            InitializeComponent();
            this.mazeBoard.DataContext = gvm;
            new MazeListener(this, this.gvm);
            this.gvm.XAdvancment = mazeBoard.XAdvancement;
            this.gvm.YAdvancment = mazeBoard.YAdvancement;

        }

        private void mazeBoard_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.mazeBoard.Window_KeyDown(sender, e);
        }

        public void ExitWindow()
        {

            MainWindow win = new MainWindow();
            this.Close();
            win.Show();
        }

        public void MainMenu_Click(Object SENDER, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ExitWindow();
            }
        }

        public void SolveMaze_Click(Object SENDER, RoutedEventArgs e)
        {
            string str = c.SolveMaze(gvm.MazeName);
            if (str != null)
            {
                copyPlayer = new Image();
                copyPlayer.Source = mazeBoard.playerImage.Source;
                copyPlayer.Width = mazeBoard.playerImage.Width;
                copyPlayer.Height = mazeBoard.playerImage.Height;
                mazeBoard.myCanvas.Children.Add(copyPlayer);
                mazeBoard.playerImage.Visibility = Visibility.Hidden;
                JObject json = JObject.Parse(str);
                stringSolution = json.GetValue("Solution").ToString();
                Position p = gvm.InitialPos;
                xPlace = p.Col * mazeBoard.XAdvancement;
                yPlace = p.Row * mazeBoard.YAdvancement;
                Canvas.SetLeft(copyPlayer, xPlace);
                Canvas.SetTop(copyPlayer, yPlace);
                
                DispatcherTimer timer = new DispatcherTimer();
                timer.Tick += new EventHandler(timer_Tick);
                timer.Interval = new TimeSpan(0, 0, 0, 0, 300);
                timer.Start();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {

            DispatcherTimer timer = (DispatcherTimer)sender;
            if (counter == stringSolution.Length)
            {
                mazeBoard.myCanvas.Children.Remove(copyPlayer);
                mazeBoard.playerImage.Visibility = Visibility.Visible;

                counter = 0;
                timer.Stop();
                return;
            }

            char move = stringSolution[counter];
            switch (move)
            {
                case '0':
                    xPlace = xPlace - mazeBoard.XAdvancement;
                    Canvas.SetLeft(copyPlayer, xPlace);
                    break;
                case '1':
                    xPlace = xPlace + mazeBoard.XAdvancement;
                    Canvas.SetLeft(copyPlayer, xPlace);
                    break;
                case '2':
                    yPlace = yPlace - mazeBoard.YAdvancement;
                    Canvas.SetTop(copyPlayer, yPlace);
                    break;
                case '3':
                    yPlace = yPlace + mazeBoard.YAdvancement;
                    Canvas.SetTop(copyPlayer, yPlace);
                    break;
            }
            counter++;

        }

        public void RestartMaze_Click(Object SENDER, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                double xInitialPosition = mazeBoard.InitialPos.Col * mazeBoard.XAdvancement;
                double yInitialPosition = mazeBoard.InitialPos.Row * mazeBoard.YAdvancement;

                gvm.RestartGame();
            }



        }
    }
}
