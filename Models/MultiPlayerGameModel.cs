using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
namespace Models
{
    public class MultiPlayerGameModel : IMultiGameModel
    {
        private Maze maze;
        private Position playerPosition;
        private Position opponentPosition;

        private string playerImageFile;
        private string exitImageFile;
        private string ip;
        private int port;
        private int searchAlgorithm;
        public MultiPlayerGameModel()
        {
            this.ip = Properties.Settings.Default.ServerIP;
            this.port = Properties.Settings.Default.ServerPort;
            this.searchAlgorithm = Properties.Settings.Default.SearchAlgorithm;

        }
        public string IP
        {
            get
            {
                return this.ip;
            }
        }

        public int Port
        {
            get
            {
                return this.port;
            }
        }

        public int SearchAlgorithm
        {
            get
            {
                return this.searchAlgorithm;
            }
        }

        public Maze MazeObject
        {
            set
            {
                maze = value;
                playerPosition = maze.InitialPos;
                opponentPosition = maze.InitialPos;
            }
        }

        public int XLocation
        {
            get
            {
                return this.playerPosition.Col;
            }
            set
            {
                this.playerPosition.Col = value;
            }
        }

        public int YLocation
        {
            get
            {
                return this.playerPosition.Row;
            }
            set
            {
                this.playerPosition.Row = value;
            }
        }

        public Position InitialPos
        {
            get
            {
                return this.maze.InitialPos;
            }
        }
        public Position GoalPos
        {
            get
            {
                return this.maze.GoalPos;
            }
        }
        public string PlayerImageFile
        {
            get
            {
                return "Resources/" + this.playerImageFile;
            }
            set
            {
                this.playerImageFile = value;
            }
        }
        public string ExitImageFile
        {
            get
            {
                return "Resources/" + this.exitImageFile;
            }
            set
            {
                this.exitImageFile = value;
            }
        }

        public string Maze
        {
            get
            {
                return this.maze.ToString();
            }
        }

        public string MazeName
        {
            get
            {
                return this.maze.Name;
            }
        }

        public int Cols
        {
            get
            {
                return this.maze.Cols;
            }
        }

        public int Rows
        {
            get
            {
                return this.maze.Rows;
            }
        }

        public int OpponentXLocation
        {
            get
            {
                return this.opponentPosition.Col;
            }

            set
            {
                this.opponentPosition.Col = value;
            }
        }

        public int OpponentYLocation
        {
            get
            {
                return this.opponentPosition.Row;
            }

            set
            {
                this.opponentPosition.Row = value;
            }
        }

        public string MoveInGame(Direction d)
        {
            int xPosition = playerPosition.Col;
            int yPosition = playerPosition.Row;
            switch (d)
            {
                case Direction.Left:
                    xPosition--;
                    break;
                case Direction.Right:
                    xPosition++;
                    break;
                case Direction.Up:
                    yPosition--;
                    break;
                case Direction.Down:
                    yPosition++;
                    break;
                default:
                    return "F";
            }
            if (xPosition >= 0 && yPosition >= 0 && xPosition < maze.Cols && yPosition < maze.Rows && maze[yPosition, xPosition] == CellType.Free)
            {
                playerPosition.Col = xPosition;
                playerPosition.Row = yPosition;
                if (xPosition == GoalPos.Col && yPosition == GoalPos.Row)
                {
                    return "W";
                }
                return "O";
            }
            return "F";
        }
        public string OtherPlayerMove(Direction d)
        {
            int xPosition = opponentPosition.Col;
            int yPosition = opponentPosition.Row;
            switch (d)
            {
                case Direction.Left:
                    xPosition--;
                    break;
                case Direction.Right:
                    xPosition++;
                    break;
                case Direction.Up:
                    yPosition--;
                    break;
                case Direction.Down:
                    yPosition++;
                    break;
                default:
                    return "F";
            }
            if (xPosition >= 0 && yPosition >= 0 && xPosition < maze.Cols && yPosition < maze.Rows && maze[yPosition, xPosition] == CellType.Free)
            {
                opponentPosition.Col = xPosition;
                opponentPosition.Row = yPosition;
                if (xPosition == GoalPos.Col && yPosition == GoalPos.Row)
                {
                    return "W";
                }
                return "O";
            }
            return "F";
        }
    }
}
