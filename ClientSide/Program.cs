using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;
using MazeLib;

namespace ClientSide
{
    class Program
    {
        static void Main()
        {

            /*
            Thread t = new Thread(ThreadProc);
            t.SetApartmentState(ApartmentState.STA);

            t.Start();

            */

            int portNum = Int32.Parse(ConfigurationManager.AppSettings["port"]);
            while (true)
            {
                Client c = new CLIClient("127.0.0.1", portNum);
                c.Connect();
            }
        }

    }
}
