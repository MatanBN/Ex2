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

namespace ClientSide
{
    class Client
    {
        private string ip;
        private int port;
        private bool playing;
        public Client(string ip, int port) {
            this.ip = ip;
            this.port = port;
            this.playing = false;
        }

        public void Connect()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ep);
                Console.WriteLine("You are connected");
                Console.WriteLine("Please choose one of the command above:\n" +
                "1. generate <name> <rows> <cols>\n" +
                "2. solve <name> <algorithm>\n" +
                "3. start <name> <rows> <cols>\n" +
                "4. list\n" +
                "5. join <name>\n" +
                "6. play <move>\n" +
                "7. close <name>");
                string input = Console.ReadLine();
                SendMessage(server, input);
                Task t = new Task(() =>
                {
                    string messagefromServer = ReadMessage(server);
                    Console.WriteLine(messagefromServer);
                    if (input.Contains("start") || input.Contains("join"))
                    {
                        if (messagefromServer != "F")
                            Playing(server);
                    }
                    server.Close();
                });
                t.Start();
                t.Wait();
            }
            catch (SocketException e) { Console.WriteLine("Unable to connect to server." + e.ToString()); }
        }
        private void SendMessage(Socket server, string message)
        {
            server.Send(Encoding.ASCII.GetBytes(message));
        }

        private string ReadMessage(Socket server)
        {
            byte[] data = new byte[1024];
            int recv = server.Receive(data);
            string stringData = Encoding.ASCII.GetString(data, 0, recv);
            return stringData;
        }
        private void Playing(Socket server)
        {
            playing = true;
            Task readTask = new Task(() =>
            {
                while (playing)
                {
                    string messagefromServer = ReadMessage(server);
                    Console.WriteLine(messagefromServer);
                    if (messagefromServer.Contains("won") || messagefromServer.Contains("Other"))
                        Console.WriteLine("press enter to return to main menu");
                        this.playing = false;
                }

            });
            string input;
            readTask.Start();
            while (playing)
            {
                input = Console.ReadLine();
                SendMessage(server, input);
                if (input.Contains("close")) {
                    this.playing = false;
                }
            }

        }
    }
}
