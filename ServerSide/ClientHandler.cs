using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net .Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    class ClientHandler : IClientHandler
    {
        private IController controller;
        private bool playing;

        public ClientHandler()
        {
            controller = new MyController();
            playing = false;
        }

        public void HandleClient(Socket client)
        {
            Task t = new Task(() =>
            {
                string command = GetCommandFromClient(client);
                Console.WriteLine(command);
                string result = ExecuteCommand(command, client);
                if (command.Contains("start") || command.Contains("join"))
                {
                    if (result != "F")
                    {
                        Playing(client);
                    }
                }
                client.Close();
            });
            t.Start();
        }

        private string GetCommandFromClient(Socket client)
        {
            byte[] data = new byte[1024];
            int recv = client.Receive(data);

            string str = Encoding.ASCII.GetString(data, 0, recv);
            return str;
        }

        private string ExecuteCommand(string commandLine, Socket client)
        {
            return controller.ExecuteCommands(commandLine, client);
        }

        private void Playing(Socket client)
        {
            string command = null;
            while (command != "close")
            {
                command = GetCommandFromClient(client);

                Console.WriteLine(command);
                
                string result = ExecuteCommand(command, client);
                if (result == "F")
                    break;
            }
        }



    }
}
