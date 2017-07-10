using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    class CloseCommand : AbstractCommand
    {
        private IModel model;
        
        public CloseCommand(IModel model)
        {
            this.model = model;
        }
        public override string Execute(string[] args, Socket client = null)
        {
            string name = args[0];
            Socket p2 = model.CloseGame(name, client);
            SendMessageToClient("Other player has existed the game", p2);
            return "F";
        }
    }
}
