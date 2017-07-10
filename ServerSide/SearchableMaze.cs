using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;

namespace ServerSide
{
    class SearchableMaze : ISearchable
    {
        private Maze m;

        public SearchableMaze(Maze maze)
        {
            m = maze;
        }

        public List<State<Position>> getAllPossibleStates(State<Position> s)
        {
            Position p = s.StateRepresentation;
            List<State<Position>> possibleStates = new List<State<Position>>();

            if (p.Col > 0)
            {
                if (m[p.Row, p.Col - 1] == CellType.Free)
                {
                    possibleStates.Add(new State<Position>(new Position(p.Row, p.Col - 1), s, s.Cost + 1));
                }
            }

            if (p.Col < m.Cols - 1)
            {
                if (m[p.Row, p.Col + 1] == CellType.Free)
                {

                    possibleStates.Add(new State<Position>(new Position(p.Row, p.Col + 1), s, s.Cost + 1));
                }
            }

            if (p.Row > 0)
            {
                if (m[p.Row - 1, p.Col] == CellType.Free)
                {

                    possibleStates.Add(new State<Position>(new Position(p.Row - 1, p.Col), s, s.Cost + 1));
                }

            }

            if (p.Row < m.Rows - 1)
            {
                if (m[p.Row + 1, p.Col] == CellType.Free)
                {

                    possibleStates.Add(new State<Position>(new Position(p.Row + 1, p.Col), s, s.Cost + 1));
                }
            }

            return possibleStates;
        }

        public State<Position> getGoalState()
        {
            return new State<Position>(m.GoalPos, null, 0);
        }

        public State<Position> getInitialState()
        {
            return new State<Position>(m.InitialPos, null, 0);
        }
    }
}
