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

        private static int player1Score = 0;
        private static int player2Score = 0;

        public static List<string> clientNames = new List<string>();

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

                Broadcast(data + " joined", data, false, false, 0);

                Console.WriteLine(data + " joined the chat room");

                clientNames.Add(data);
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

        public static void Broadcast(string msg, string uname, bool flag, bool isScore, int clientWhich)
        {
            foreach (var item in ClientList)
            {
                var broadcastSocket = item.Value;
                var m = "";
                if (isScore)
                {
                    m = msg; 
                } else
                {
                    m = flag ? uname + " says: " + msg : msg;
                }
                item.Value.WriteString(m, isScore, clientWhich);
            }
        }
        
        public static void UpdateAnswer(string ans, int clientWhich)
        { 
            foreach (var item in ClientList)
            {
                var answerSocket = item.Value;
                if (clientWhich == 1)
                {
                    if (ans == "correct#")
                        player1Score++;

                    item.Value.WriteString(player1Score.ToString(),true, 1);
                }
                else if (clientWhich == 2)
                {
                    if (ans == "correct#")
                        player2Score++;
                    item.Value.WriteString(player2Score.ToString(), true, 2);
                }
                


            }
        }

        

    }
}
