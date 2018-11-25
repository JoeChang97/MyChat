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
        // Keeping track of the clients (there should be only 2)
        public static Dictionary<string, TcpClient> ClientList = new Dictionary<string, TcpClient>();
        private static int player1Score = 0;
        private static int player2Score = 0;
        
        public static List<string> clientNames = new List<string>();

        static void Main()
        {
            // Start the server socket
            var socket = new TcpListener(IPAddress.Any, 8888);
            socket.Start();

            Console.WriteLine("Server started");

            while (true)
            {
                
                var client = socket.AcceptTcpClient();

                string data = client.ReadString();
                // a client joined, add to the list
                ClientList.Add(data, client);
                // show the client in the chat
                Broadcast(data + " joined", data, false, false, 0);

                Console.WriteLine(data + " joined the chat room");
                // add client name to the list
                clientNames.Add(data);
                // Start a new client thread
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
        /// <param name="flag">True if it is a chat, false if is a connect</param>
        /// <param name="isScore">True if it is a score (number)</param>
        /// <param name="clientWhich">Which client triggered the broadcast</param>

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
                    // if it is a chat text, add "client says.."
                    m = flag ? uname + " says: " + msg : msg;
                }
                item.Value.WriteString(m, isScore, clientWhich);
            }
        }

        /// <summary>
        /// A simple update answer function check the answer from stream and update scores
        /// If it is a correct answer, increment scores. Show the new scores to all clients
        /// </summary>
        /// <param name="ans">The answer, either true or false</param>
        /// <param name="clientWhich">Which client played</param>
        
        public static void UpdateAnswer(string ans, int clientWhich)
        {
            Console.WriteLine(ClientList);
            // Is it player 1?
            if (clientWhich == 1)
            {
                if (ans == "correct#")
                {
                    player1Score++;
                }

                // show the new scores to all clients
                foreach (var item in ClientList)
                {
                    item.Value.WriteString(player1Score.ToString(), true, 1);
                }
            }
            // Is it player 2?
            else if (clientWhich == 2)
            {
                if (ans == "correct#")
                {
                    player2Score++;
                }

                // show the new scores to all clients
                foreach (var item in ClientList)
                {
                    item.Value.WriteString(player2Score.ToString(), true, 2);
                }
            }
        }
    }
}
