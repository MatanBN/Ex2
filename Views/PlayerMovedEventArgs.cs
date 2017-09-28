using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Views
{
    public class PlayerMovedEventArgs : EventArgs
    {
        private Direction direction;
        public Direction Direction
        {
            get
            {
                return this.direction;
            }
            private set
            {
                this.direction = value;
            }
        }

        public PlayerMovedEventArgs(Direction direction)
        {
            Direction = direction;
        }
    }
}
