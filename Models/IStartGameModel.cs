using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public interface IStartGameModel
    {
        string MazeName { get; set; }
        int MazeCols { get; set; }
        int MazeRows { get; set; }
    }
}
