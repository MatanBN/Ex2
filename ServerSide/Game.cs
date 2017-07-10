using MazeLib;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    public class Game
    {
        private Socket p1;
        private Socket p2;
        private Dictionary<Socket, Player> players;
        private Maze m;
        private bool started;

        public Game(Maze m, Socket p1)
        {
            this.m = m;
            this.players = new Dictionary<Socket, Player>();
            this.players.Add(p1, new Player(m,m.InitialPos));
            this.started = false;
            this.p1 = p1;
        }

        public Socket P2
        {
            set {
                this.players.Add(value,new Player(m,m.InitialPos));
                this.p2 = value;
            }
            get
            {
                return this.p2;
            }
        }

        public Socket P1
        {
            get { return this.p1; }
        }

        public Maze Maze
        {
            get
            {
                return this.m;
            }
        } 



        public bool Started
        {
            get { return this.started; }
        }

        public void StartGame()
        {
            this.started = true;
        }

        public string Move(Socket s, Direction d)
        {
            Player p;
            players.TryGetValue(s, out p);
            string move = p.Move(d);
            if (move == "W")
                this.started = false;
            return move;
        }
    }
}
