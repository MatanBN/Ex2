using System;
using Medallion.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;
using System.Configuration;

namespace ServerSide
{
    class Program
    {
        static void Main(string[] args)
        {
            //CompareSolvers();
            int portNum = Int32.Parse(ConfigurationManager.AppSettings["port"]);

            Server s = new Server(portNum, new ClientHandler(), "127.0.0.1");
            s.Start();
            
        }

        public static void CompareSolvers()
        {
            DFSMazeGenerator mazeGen = new DFSMazeGenerator();
            Maze maze = mazeGen.Generate(10,10);
            SearchableMaze sm = new SearchableMaze(maze);
            Console.WriteLine(maze);
            BFSSearcher bfs= new BFSSearcher();
            DFSSearcher dfs = new DFSSearcher();
            Solution BFSSolution = bfs.search(sm);
            Solution DFSSolution = dfs.search(sm);
           // Console.WriteLine($"Nodes: {BFSNodes}");
           // Console.WriteLine($"Nodes: {DFSNodes}");
            int BFSNodes = bfs.getNumberOfNodesEvaluated();
            int DFSNodes = dfs.getNumberOfNodesEvaluated();

        }
    }
}
