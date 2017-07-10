using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    class Server
    {
        private int port;
        private Socket newsock;
        private IClientHandler ch;

        private string ip;
        public Server(int port, IClientHandler ch, string ip)
        {
            this.port = port;
            this.ch = ch;
            this.ip = ip;
        }
        public void Start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            newsock.Bind(ep);
            Task task = new Task(() =>
            {
                while (true)
                {
                    Console.WriteLine("Waiting for connections...");
                    newsock.Listen(10);
                    Socket client = newsock.Accept();
                    Console.WriteLine("Got Connection");
                    ch.HandleClient(client);
                }


            });
            task.Start();
            task.Wait();
            Console.WriteLine("Server stopped");
            newsock.Close();
            }
            
        }
        //public void Stop()
        //{
         //   listener.Stop();
       // }
    
}
