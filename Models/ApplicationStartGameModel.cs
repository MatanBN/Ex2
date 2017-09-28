using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ApplicationStartGameModel : IStartGameModel
    {

        private int mazeCols;
        private int mazeRows;
        private string mazeName;
        public ApplicationStartGameModel()
        {
            this.mazeCols = Properties.Settings.Default.MazeCols;
            this.mazeRows = Properties.Settings.Default.MazeRows;
            
        }
        public string MazeName
        {
            get
            {
                return this.mazeName;
            }

            set
            {
                this.mazeName = value;
            }
        }

        public int MazeCols
        {
            get
            {
                return this.mazeCols;
            }

            set
            {
                this.mazeCols = value;
            }
        }

        public int MazeRows
        {
            get
            {
                return this.mazeRows;
            }

            set
            {
                this.mazeRows = value;
            }
        }
    }
}
