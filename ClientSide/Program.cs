using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ViewModels;
using Views;
using Models;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace ClientSide
{
    class Program
    {
        [STAThread]
        static void Main()
        {

            MainWindow window = new MainWindow();
            System.Windows.Application wpfApplication = new System.Windows.Application();
            wpfApplication.Run(window);
            /*
            Thread t = new Thread(ThreadProc);
            t.SetApartmentState(ApartmentState.STA);

            t.Start();


            int portNum = Int32.Parse(ConfigurationManager.AppSettings["port"]);
            while (true)
            {
                Client c = new Client("127.0.0.1", portNum);
                c.Connect();
            }
            */
        }

    }
}
