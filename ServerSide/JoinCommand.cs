using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    class JoinCommand : AbstractCommand
    {
        private IModel model;
        public JoinCommand(IModel model)
        {
            this.model = model;
        }

        public override string Execute(string[] args, Socket client = null)
        {
            string name = args[0];
            Game g = model.JoinMazeGame(name, client);
            if (g == null)
            {
                SendMessageToClient("F", client);
            }
            else
            {
                string jsonMaze = g.Maze.ToJSON();
                SendMessageToClient(jsonMaze, g.P1);
                SendMessageToClient(jsonMaze, g.P2);
            }
            return null;
        }
    }
}
