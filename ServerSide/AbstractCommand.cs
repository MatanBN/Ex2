using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    public abstract class AbstractCommand : ICommand
    {
        public abstract string Execute(string[] args, Socket client = null);

        protected void SendMessageToClient(string message, Socket client)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            client.Send(data, data.Length, SocketFlags.None);
        }
    }
}
