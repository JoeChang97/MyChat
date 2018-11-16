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
            var thread = new Thread(DoChat);
            thread.Start();
        }

        private void DoChat()
        {
            while (true)
            {
                try
                {
                    string dataFromClient = _client.ReadString();
                    Program.Broadcast(dataFromClient, _clientname, true);
                    Console.WriteLine(dataFromClient);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        private void UpdateScore()
        {
            while (true)
            {
                try
                {

                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
