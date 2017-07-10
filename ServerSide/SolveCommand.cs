using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    class SolveCommand : AbstractCommand
    {
        private IModel model;
        public SolveCommand(IModel model)
        {
            this.model = model;
        }

        public override string Execute(string[] args, Socket client = null)
        {
            string name = args[0];
            int algorithm = int.Parse(args[1]);
            Maze m = model.GetMazeByName(name);
            
            if (m == null)
            {
                return "No Such Maze";
            }
            Solution sol = model.GetMazeSolution(name);
            if (sol == null)
            {
                ISearcher searcher;
                SearchableMaze sm = new SearchableMaze(m);
                if (algorithm == 1)
                {
                    searcher = new DFSSearcher();
                }

                else if (algorithm == 0)
                {
                    searcher = new BFSSearcher();
                }
                else
                {
                    return null;
                }
                sol = model.search(sm, searcher);
                sol.Name = name;
                model.AddMazeSolution(name, sol);
            }
            string jsonSol = sol.toJSON();
            SendMessageToClient(jsonSol, client);
            return jsonSol;

        }
    }
}
