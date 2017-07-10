using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace ClientSide
{
    class Program
    {
        static void Main(string[] args)
        {
            int portNum = Int32.Parse(ConfigurationManager.AppSettings["port"]);
            while (true)
            {
                Client c = new Client("127.0.0.1", portNum);
                c.Connect();
            }
        }
    }
}
