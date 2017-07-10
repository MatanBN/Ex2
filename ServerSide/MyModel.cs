using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;
using MazeGeneratorLib;
using System.Net.Sockets;
using Newtonsoft.Json.Linq;

namespace ServerSide
{
    class MyModel : IModel
    {
        private Dictionary<string, Pair<Maze, Solution>> mazes;
        private Dictionary<Socket, Game> games;


        public MyModel()
        {
            mazes = new Dictionary<string, Pair<Maze, Solution>>();
            games = new Dictionary<Socket, Game>();
        }
        public Maze GenerateMaze(string name, int rows, int cols)
        {
            DFSMazeGenerator mazeGen = new DFSMazeGenerator();
            Maze maze = mazeGen.Generate(rows, cols);
            maze.Name = name;
            mazes.Add(name, new Pair<Maze, Solution>(maze, null));
            return maze;
        }

        public Solution GetMazeSolution(string name)
        {
            Pair<Maze, Solution> tuple;
            if (!(mazes.TryGetValue(name, out tuple)))
            {
                return null;
            }
            return tuple.Item2;
        }

        public void AddMazeSolution(string name, Solution sol)
        {
            Pair<Maze, Solution> tuple;
            if ((mazes.TryGetValue(name, out tuple)))
            {
                tuple.Item2 = sol;
            }
        }

        public bool StartMazeGame(string name, Socket p1)
        {
            Game game;
            if (!(games.TryGetValue(p1, out game)))
            {
                games.Add(p1, new Game(GetMazeByName(name), p1));
                return true;
            }
            return false;
        }

        public Game JoinMazeGame(string name, Socket p2)
        {
            foreach (Game game in games.Values)
            {
                string gameName = game.Maze.Name;
                if (gameName == name)
                {
                    if (!game.Started)
                    {
                        game.P2 = p2;
                        game.StartGame();
                        games.Add(p2, game);
                        return game;
                    }
                }
            }
            return null;
        }
        public List<string> GetListOfGames()
        {
            List<string> openGames = new List<string>();
            foreach(Game game in games.Values) {
                if (!game.Started)
                {
                    openGames.Add(game.Maze.Name);
                }
            }
            return openGames;
        }
        public Solution search(ISearchable p, ISearcher algo)
        {
            Solution sol = algo.search(p);
            return sol;

        }

        public Maze GetMazeByName(string name)
        {
            Pair<Maze, Solution> tuple;
            if(!(mazes.TryGetValue(name, out tuple))) {
                return null;
            }
            return tuple.Item1;
        }

        public Game MoveInGame(Socket s, Direction d)
        {
            Game game;
            if ((games.TryGetValue(s, out game)))
            {
                string move = game.Move(s, d);
                if (move == "F")
                    return null;
                if (move == "W")
                {
                    games.Remove(s);
                    DeletePairGame(game);

                }

            }
            return game;
        }

        public Socket CloseGame(string name, Socket s)
        {
            Game game;
            if ((games.TryGetValue(s, out game)))
            {
                games.Remove(s);
                DeletePairGame(game);
                if (s == game.P1)
                {
                    return game.P2;
                }
                else
                {
                    return game.P1;
                }
            }
            return null;
        }

        private void DeletePairGame(Game game)
        {
            foreach (KeyValuePair<Socket, Game> g in games)
            {
                string gameName = g.Value.Maze.Name;
                if (game.Maze.Name == gameName)
                {
                    games.Remove(g.Key);
                    break;
                }
            }
        }
    }

}
