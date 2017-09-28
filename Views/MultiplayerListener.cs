using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModels;
using ClientSide;
using MazeLib;

namespace Views
{
    public class MultiplayerListener
    {
        private MultiPlayerGame spm;
        private MultiPlayerGameViewModel gvm;

        private GUIClient c;
        public bool gameOver = false;
        public MultiplayerListener(MultiPlayerGame spm, MultiPlayerGameViewModel gvm, GUIClient c)
        {
            this.spm = spm;
            this.gvm = gvm;
            this.c = c;
            this.spm.myMazeBoard.playerMoved += MazeBoard_MyPlayerMoved;
            this.c.playerMoved += MazeBoard_OtherPlayerMoved;
        }

        private void MazeBoard_MyPlayerMoved(object sender, PlayerMovedEventArgs e)
        {
            string moveResult = gvm.MoveInGame(e.Direction);
            if (moveResult != "F")
                c.PlayGame(e.Direction.ToString().ToLower());
            if (moveResult == "W")
            {
                 spm.ExitWindow();
            }
        }



        public void MazeBoard_OtherPlayerMoved(object sender, Direction d)
        {
            if (d == Direction.Unknown)
            {
                gameOver = true;
            }
            else
            {
                string moveResult = gvm.OtherPlayerMove(d);
            }
        }
    }
}
