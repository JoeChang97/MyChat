using MyChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    class Program
    {
        public static Dictionary<string, TcpClient> ClientList = new Dictionary<string, TcpClient>();

        static void Main()
        {
            var socket = new TcpListener(IPAddress.Any, 8888);
            socket.Start();

            Console.WriteLine("Server started");

            while (true)
            {
                var client = socket.AcceptTcpClient();

                string data = client.ReadString();

                ClientList.Add(data, client);

                
                Broadcast(data + " joined", data, false);

                Console.WriteLine(data + " joined the chat room");

                var clientthread = new ClientThread();
                clientthread.StartClient(client, data);
            }


        }

        /// <summary>
        /// A simple broadcast message function that resides here to allow the clients to broadcast
        /// incomming messages to everyone. 
        /// </summary>
        /// <param name="msg">The message to broadcast</param>
        /// <param name="uname">The user's name who sent it</param>
        /// <param name="flag"></param>

        public static void Broadcast(string msg, string uname, bool flag)
        {
            foreach (var item in ClientList)
            {
                var broadcastSocket = item.Value;
                var m = flag ? uname + " says: " + msg : msg;
                item.Value.WriteString(m, false);
                
            }
        }

        public static void SyncScore(int score, bool flag)
        {
            foreach (var item in ClientList)
            {
                var broadcastSocket = item.Value;
                item.Value.WriteString(score.ToString(), true);
            }
        }

    }
}
