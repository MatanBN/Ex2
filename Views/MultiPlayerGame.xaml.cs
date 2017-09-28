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
using ClientSide;
using System.ComponentModel;

namespace Views
{


    /// <summary>
    /// Interaction logic for MultiPlayerGame.xaml
    /// </summary>
    public partial class MultiPlayerGame : Window
    {
        private MultiPlayerGameViewModel gvm;
        private MultiplayerListener listener;
        private GUIClient c;

        public MultiPlayerGame(MultiPlayerGameViewModel gvm, GUIClient c)
        {
            this.gvm = gvm;
            this.DataContext = gvm;
            this.c = c;
            InitializeComponent();
            this.myMazeBoard.DataContext = gvm;
            this.opponentsMazeBoard.DataContext = gvm;
            this.gvm.XAdvancment = myMazeBoard.XAdvancement;
            this.gvm.YAdvancment = myMazeBoard.YAdvancement;
            Binding picX = new Binding();
            picX.Path = new PropertyPath("OpponentPicX");
            picX.Source = gvm;  // view model?
            Binding picY = new Binding();
            picY.Path = new PropertyPath("OpponentPicY");
            picY.Source = gvm;  // view model?
            BindingOperations.SetBinding(opponentsMazeBoard.playerImage, Canvas.LeftProperty, picX);
            BindingOperations.SetBinding(opponentsMazeBoard.playerImage, Canvas.TopProperty, picY);
            listener = new MultiplayerListener(this, this.gvm, this.c);
            Closing += OnWindowClosing;


        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            if (!listener.gameOver)
                c.CloseGame(gvm.MazeName);

        }

        public void BackToMainClick(Object SENDER, RoutedEventArgs e)
        {
            ExitWindow();
        }

        private void mazeBoard_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.myMazeBoard.Window_KeyDown(sender, e);
        }

        private void changeWindow(Window w)
        {
            Application.Current.MainWindow = w;
            this.Close();
            w.Show();
        }
        public void ExitWindow()
        {

            MainWindow win = new MainWindow();
            this.Close();
            win.Show();
        }

    }
}
