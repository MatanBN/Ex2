using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    public class Player
    {
        private Position pos;
        private Maze m;
        public Player(Maze m,Position pos)
        {
            this.pos = pos;
            this.m = m;
        }

        public string Move(Direction d)
        {
            int x = pos.Row;
            int y = pos.Col;
            if (d == Direction.Down)
                x = pos.Row + 1;
            else if (d == Direction.Up)
                x = pos.Row - 1;
            else if (d == Direction.Left)
                y = pos.Col - 1;
            else 
                y = pos.Col + 1;
            if (m[x,y] == CellType.Wall)
            {
                return "F";
            }
            pos.Row = x;
            pos.Col = y;
            if (m.GoalPos.Row == x && m.GoalPos.Col == y)
                return "W";
            return d.ToString();

        }
    }
}
