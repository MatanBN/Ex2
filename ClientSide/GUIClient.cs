using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using MazeLib;
using System.Windows;
using Newtonsoft.Json.Linq;

namespace ClientSide
{
    public class GUIClient : Client
    {
        private GameViewModel gvm;
        public event EventHandler<Direction> playerMoved;
        public GUIClient(GameViewModel gvm) : base(gvm.IP, gvm.Port)
        {
            this.gvm = gvm;
        }

        public void GenerateMaze(int cols, int rows, string mazeName)
        {

            try
            {
                base.Connect();
                string message = "generate " + mazeName + " " + rows.ToString() + " " + cols.ToString();
                SendMessage(message);
                Task t = new Task(() =>
                {
                    string messagefromServer = ReadMessage();
                    Maze m = Maze.FromJSON(messagefromServer);
                    gvm.MazeObject = m;
                    gvm.PlayerImageFile = "SuperMario.png";
                    gvm.ExitImageFile = "Castle.png";
                    server.Close();
                });
                t.Start();
                t.Wait();

            }
            catch (SocketException e) {
                System.Windows.MessageBox.Show("Unable to connect to server", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        public string SolveMaze(string mazeName)
        {
            string messagefromServer = null;

            try
            {
                base.Connect();
                string message = "solve " + mazeName + " " + gvm.SearchAlgorithm.ToString();
                SendMessage(message);
                Task t = new Task(() =>
                {
                    messagefromServer = ReadMessage();
                    server.Close();
                });
                t.Start();
                t.Wait();
                return messagefromServer;
            }
            catch (SocketException e)
            {
                System.Windows.MessageBox.Show("Unable to connect to server", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                return null;
            }
        }

        public string ListGames()
        {
            string messagefromServer = null;

            try
            {
                base.Connect();
                string message = "list";
                SendMessage(message);
                Task t = new Task(() =>
                {
                    messagefromServer = ReadMessage();
                    server.Close();
                });
                t.Start();
                t.Wait();
            }
            catch (SocketException e)
            {
                System.Windows.MessageBox.Show("Unable to connect to server", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Asterisk);

            }
            return messagefromServer;
        }

        public string JoinGame(string mazeName)
        {
            string mazeString = null;
            try
            {
                base.Connect();
                string message = "join " + mazeName;
                SendMessage(message);
                Task t = new Task(() =>
                {
                    mazeString = ReadMessage();
                });
                t.Start();
                t.Wait();
                Task readTask = new Task(() =>
                {
                    while (true)
                    {
                        string messagefromServer = ReadMessage();
                        if (messagefromServer.Contains("won"))
                        {
                            MessageBoxResult result = System.Windows.MessageBox.Show(messagefromServer, "Confirmation", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            playerMoved?.Invoke(this, Direction.Unknown);

                            if (result == MessageBoxResult.OK)
                            {
                                server.Close();
                                break;
                            }


                        }
                        else if (messagefromServer.Contains("Other"))
                        {
                            MessageBoxResult resultOther = System.Windows.MessageBox.Show("You're opponent has left the game", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            playerMoved?.Invoke(this, Direction.Unknown);
                            if (resultOther == MessageBoxResult.OK)
                            {
                                server.Close();

                                break;
                            }
                        }
                        JObject json = JObject.Parse(messagefromServer);
                        string directionString = json.GetValue("Direction").ToString();
                        Direction d = Direction.Left;
                        switch (directionString)
                        {
                            case "right":
                                d = Direction.Right;
                                break;
                            case "up":
                                d = Direction.Up;
                                break;
                            case "down":
                                d = Direction.Down;
                                break;
                        }
                        playerMoved?.Invoke(this, d);
                    }

                });
                readTask.Start();
            }
            catch (SocketException e)
            {
                System.Windows.MessageBox.Show("Unable to connect to server", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            return mazeString;
        }

        public string StartGame(int cols, int rows, string mazeName)
        {
            string mazeString = null;
            try
            {
                GenerateMaze(cols, rows, mazeName);

                base.Connect();
                string message = "start " + mazeName + " " + rows.ToString() + " " + cols.ToString();
                SendMessage(message);
                Task t = new Task(() =>
                {
                    mazeString = ReadMessage();
                });
                t.Start();
                t.Wait();
                Task readTask = new Task(() =>
                {
                    while (true)
                    {
                        string messagefromServer = ReadMessage();
                        if (messagefromServer.Contains("won"))
                        {
                            MessageBoxResult result = System.Windows.MessageBox.Show(messagefromServer, "Confirmation", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            playerMoved?.Invoke(this, Direction.Unknown);

                            if (result == MessageBoxResult.OK)
                            {
                                server.Close();

                                break;
                            }


                        }
                        else if (messagefromServer.Contains("Other"))
                        {
                            MessageBoxResult resultOther = System.Windows.MessageBox.Show("You're opponent has left the game", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            playerMoved?.Invoke(this, Direction.Unknown);
                            if (resultOther == MessageBoxResult.OK)
                            {
                                server.Close();

                                break;
                            }
                        }
                        if (messagefromServer == "")
                        {
                            break;
                        }
                        JObject json = JObject.Parse(messagefromServer);
                        string directionString = json.GetValue("Direction").ToString();
                        Direction d = Direction.Left;
                        switch (directionString)
                        {
                            case "right":
                                d = Direction.Right;
                                break;
                            case "up":
                                d = Direction.Up;
                                break;
                            case "down":
                                d = Direction.Down;
                                break;
                        }
                        playerMoved?.Invoke(this, d);
                    }

                });
                readTask.Start();
            }
            catch (SocketException e)
            {
                System.Windows.MessageBox.Show("Unable to connect to server", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            return mazeString;
        }

        public Task readTask()
        {
            return null;
        }

        public void PlayGame(string move)
        {
            try
            {
                string message = "play " + move;
                SendMessage(message);
            }
            catch (SocketException e)
            {
                System.Windows.MessageBox.Show("Lost connection to server", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        public void CloseGame(string game)
        {
            try
            {
                string message = "close " + game;
                SendMessage(message);
                server.Close();
            }
            catch (SocketException e)
            {
                System.Windows.MessageBox.Show("Lost connection to server", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }
    }
}
