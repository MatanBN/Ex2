using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public interface IMultiGameModel
    {
        Maze MazeObject { set; }
        int Rows { get; }
        int Cols { get; }
        string Maze { get; }
        string MazeName { get; }
        Position InitialPos { get; }
        Position GoalPos { get; }
        string PlayerImageFile { get; set; }
        string ExitImageFile { get; set; }
        int XLocation { get; set; }
        int YLocation { get; set; }
        string MoveInGame(Direction d);
        string OtherPlayerMove(Direction d);

        string IP { get; }
        int Port { get; }
        int SearchAlgorithm { get; }
        int OpponentXLocation { get; set; }
        int OpponentYLocation { get; set; }

    }
}
