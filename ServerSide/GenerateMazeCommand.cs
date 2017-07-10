using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    class GenerateMazeCommand : AbstractCommand
    {
        private IModel model;
        
        public GenerateMazeCommand(IModel model)
        {
            this.model = model;
        }

        public override string Execute(string[] args, Socket client = null)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);

            Maze maze = model.GenerateMaze(name, rows, cols);
            Console.WriteLine(maze.ToString());
            string jsonMaze = maze.ToJSON();
            SendMessageToClient(jsonMaze, client);
            return jsonMaze;
        }


    }
}
