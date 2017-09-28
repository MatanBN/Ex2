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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModels;
using Models;
namespace Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void ChangeWindow(Window w)
        {

            Application.Current.MainWindow = w;
            this.Close();
            w.Show();
        }

        private void SP_Click(object sender, RoutedEventArgs e)
        {
            Single_Player sp = new Single_Player(new StartGameViewModel(new ApplicationStartGameModel()));
            ChangeWindow(sp);
        }

        private void MP_Click(object sender, RoutedEventArgs e)
        {
            Multi_Player mp = new Multi_Player(new StartGameViewModel(new ApplicationStartGameModel()));
            ChangeWindow(mp);
        }

        private void S_Click(Object SENDER, RoutedEventArgs e)
        {
            Settings s = new Settings(new SettingsViewModel(new ApplicationSettingsModel()));
            ChangeWindow(s);
        }
    }
}
