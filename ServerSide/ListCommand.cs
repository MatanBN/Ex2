using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    class ListCommand : AbstractCommand
    {
        IModel model;

        public ListCommand(IModel model)
        {
            this.model = model;
        }
        public override string Execute(string[] args, Socket client = null)
        {
            string openGames = ParseGames(model.GetListOfGames());
            SendMessageToClient(openGames, client);
            return openGames;
        }

        private string ParseGames(List<string> openGames)
        {
            StringBuilder sob = new StringBuilder();
            sob.Append("[\n");
            foreach (string game in openGames)
            {
                sob.Append(game);
                sob.Append(",\n");
            }
            sob.Append("]");
            return sob.ToString();
        }
    }
}
