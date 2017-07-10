using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using System.Net.Sockets;

namespace ServerSide
{
    class MyController : IController
    {
        private Dictionary<string, ICommand> commands;

        private IModel m;
        private IView v;

        public MyController()
        {
            m = new MyModel();
            commands = new Dictionary<string, ICommand>();
            commands.Add("generate", new GenerateMazeCommand(m));
            commands.Add("solve", new SolveCommand(m));
            commands.Add("start", new StartCommand(m));
            commands.Add("list", new ListCommand(m));
            commands.Add("join", new JoinCommand(m));
            commands.Add("play", new PlayCommand(m));
            commands.Add("close", new CloseCommand(m));
        }
        public void setModel(IModel m) { this.m = m; }
        public void setView(IView v) { this.v = v; }
        public void setSolution(Solution s)
        {
            v.displaySolution(s);
        }


        public string ExecuteCommands(string commandLine, Socket client)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (!commands.ContainsKey(commandKey))
                return "Command not found";
            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];
            return command.Execute(args, client);
        }
    }

}
