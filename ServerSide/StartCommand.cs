using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    public class StartCommand : AbstractCommand
    {
        private IModel model;
        public StartCommand(IModel m)
        {
            this.model = m;
        }
        public override string Execute(string[] args, Socket client = null)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            if (!model.StartMazeGame(name, client))
            {
                SendMessageToClient("F", client);
            }
            return null;
        }
    }
}
