using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;
using System.Net.Sockets;

namespace ServerSide
{
    public interface IModel
    {
        Solution search(ISearchable p, ISearcher searcher);
        Maze GenerateMaze(string name, int rows, int cols);

        Maze GetMazeByName(string name);

        Solution GetMazeSolution(string name);
        void AddMazeSolution(string name, Solution sol);
        List<string> GetListOfGames();
        Game JoinMazeGame(string name, Socket p2);
        bool StartMazeGame(string name, Socket p1);
        Game MoveInGame(Socket s, Direction d);
        Socket CloseGame(string name, Socket s);

    }

}
