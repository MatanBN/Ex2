using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using System.Net.Sockets;

namespace ServerSide
{
    public interface IController
    {
        string ExecuteCommands(string commandLine, Socket client);
        void setModel(IModel m);
        void setView(IView v);
        void setSolution(Solution s);
    }

}
