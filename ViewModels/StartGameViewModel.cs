using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class StartGameViewModel : ViewModel
    {
        private IStartGameModel spm;
        public StartGameViewModel(IStartGameModel spm)
        {
            this.spm = spm;

        }

        public string MazeName
        {
            get
            {
                return spm.MazeName;
            }
            set
            {
                spm.MazeName = value;
                NotifyPropertyChanged("MazeName");
            }
        }

        public int MazeCols
        {
            get
            {
                return spm.MazeCols;
            }
            set
            {
                spm.MazeCols = value;
                NotifyPropertyChanged("MazeCols");
            }
        }

        public int MazeRows
        {
            get
            {
                return spm.MazeRows;
            }
            set
            {
                spm.MazeRows = value;
                NotifyPropertyChanged("MazeRows");
            }
        }
    }
}
