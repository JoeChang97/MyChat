using MyChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatServer
{
    class ClientThread
    {

        private TcpClient _client;
        private string _clientname;

        public void StartClient(TcpClient clientSocket, string clientName)
        {
            _clientname = clientName;
            _client = clientSocket;
            // start the thread
            var thread = new Thread(SyncClient);
            thread.Start();
        }

        /// <summary>
        /// A thread that check the string from stream and perform other functions accordingly
        /// If the string is an answer, call checking asnwer functions
        /// If it is a player chatting, broadcast the text
        /// </summary>
        private void SyncClient()
        {
            while (true)
            {   try
                {
                    string dataFromClient = _client.ReadString();
                    // Check if this is just an answer
                    if (dataFromClient.Contains('#'))
                    {
                        if (_clientname == Program.clientNames[0])
                        {
                            // It's answer from player 1
                            Program.UpdateAnswer(dataFromClient, 1);
                        }
                        else if (_clientname == Program.clientNames[1])
                        {
                            // It's answer from player 2
                            Program.UpdateAnswer(dataFromClient, 2);
                        }
                    } else
                    {   // It is a text
                        if (_clientname == Program.clientNames[0])
                        {
                            // It's a text from player 1
                            Program.Broadcast(dataFromClient, _clientname, true, false, 1);
                        }
                        else if (_clientname == Program.clientNames[1])
                        {
                            // It's a text from player 2
                            Program.Broadcast(dataFromClient, _clientname, true, false, 2);
                        }
                        else
                        {
                            // It's not a text from players?
                            Program.Broadcast(dataFromClient, _clientname, true, false, 0);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
