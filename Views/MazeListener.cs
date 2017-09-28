using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ViewModels;
using ClientSide;
namespace Views
{
    public class MazeListener
    {
        private SinglePlayerGame spm;
        private GameViewModel gvm;
        public MazeListener(SinglePlayerGame spm, GameViewModel gvm)
        {
            this.spm = spm;
            this.gvm = gvm;
            this.spm.mazeBoard.playerMoved += MazeBoard_PlayerMoved;
        }

        private void MazeBoard_PlayerMoved(object sender, PlayerMovedEventArgs e)
        {
            string moveResult = gvm.MoveInGame(e.Direction);

            if (moveResult == "W") {
                MessageBoxResult result = MessageBox.Show("You've found the castle! :) Click ok to go back to main menu", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    spm.ExitWindow();
                }
            }
        }
    }
}