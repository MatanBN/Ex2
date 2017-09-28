using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ClientSide
{
    public abstract class Client
    {
        private string ip;
        private int port;
        protected bool playing;
        protected Socket server;
        public Client(string ip, int port) {
            this.ip = ip;
            this.port = port;
            this.playing = false;
        }

        public virtual void Connect()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ep);
            }
            catch (SocketException e)
            {
                throw e;
            }


        }

        
        protected void SendMessage(string message)
        {
            server.Send(Encoding.ASCII.GetBytes(message));
        }

        public string ReadMessage()
        {
            try
            {
                byte[] data = new byte[1024];
                int recv = server.Receive(data);
                string stringData = Encoding.ASCII.GetString(data, 0, recv);
                return stringData;
            }
            catch (SocketException e)
            {
                return null;
            }
        }

    }
}
