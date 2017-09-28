using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientSide
{
    public class CLIClient : Client
    {
        public CLIClient(string ip, int port) : base(ip, port)
        {
        }

        public override void Connect()
        {
            try
            {
                base.Connect();


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
                base.SendMessage(input);
                Task t = new Task(() =>
                {
                    string messagefromServer = ReadMessage();
                    Console.WriteLine(messagefromServer);
                    if (input.Contains("start") || input.Contains("join"))
                    {
                        if (messagefromServer != "F")
                            Playing();
                    }
                    server.Close();
                });
                t.Start();
                t.Wait();
            }
            catch (SocketException e) { Console.WriteLine("Unable to connect to server." + e.ToString()); }
        }
        protected void Playing()
        {
            playing = true;
            Task readTask = new Task(() =>
            {
                while (playing)
                {
                    string messagefromServer = ReadMessage();
                    Console.WriteLine(messagefromServer);
                    if (messagefromServer.Contains("won") || messagefromServer.Contains("Other")) { 
                        Console.WriteLine("press enter to return to main menu");
                        this.playing = false;
                    }
                }

            });
            string input;
            readTask.Start();
            while (playing)
            {
                input = Console.ReadLine();
                SendMessage(input);
                if (input.Contains("close"))
                {
                    this.playing = false;
                }
            }

        }
    }
}
