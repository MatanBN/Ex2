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
namespace Views
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private SettingsViewModel vm;
        public Settings(SettingsViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            DataContext = vm;
        }

        private void ExitWindow()
        {

            MainWindow win = new MainWindow();
            this.Close();
            win.Show();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            vm.SaveSettings();
            ExitWindow();
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ExitWindow();
        }

        private void ComboBoxItem_TargetUpdated(object sender, DataTransferEventArgs e)
        {

        }
    }
}
