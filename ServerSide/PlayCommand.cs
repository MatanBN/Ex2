using MazeLib;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    class PlayCommand : AbstractCommand
    {
        private IModel model;
        public PlayCommand(IModel model)
        {
            this.model = model;
        }
        public override string Execute(string[] args, Socket client = null)
        {
            string move = args[0];
            Direction d = Direction.Left;
            if (move == "right")
            {
                d = Direction.Right;
            }
            else if (move == "up")
            {
                d = Direction.Up;
            }
            else if (move == "down")
            {
                d = Direction.Down;
            }
            Game g = model.MoveInGame(client, d);
            if (g != null)
            {
                if (g.Started == false)
                {
                    string winner;
                    if (g.P1 == client)
                    {
                        winner = "Player 1 has won";
                    }
                    else
                    {
                        winner = "Player 2 has won";
                    }
                    SendMessageToClient(winner, g.P1);
                    SendMessageToClient(winner, g.P2);
                    return "F";
                }
                JObject json = new JObject();
                json.Add("Name", g.Maze.Name);
                json.Add("Direction", move);
                if (g.P1 == client)
                {
                    SendMessageToClient(json.ToString(), g.P2);
                }
                else
                {
                    SendMessageToClient(json.ToString(), g.P1);
                }
            }
            return null;
        }
    }
}
