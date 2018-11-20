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
            var thread = new Thread(SyncClient);
            thread.Start();
        }

        private void SyncClient()
        {
            while (true)
            {
                try
                {
                    string dataFromClient = _client.ReadString();
                    // Check if this is just an answer
                    if (dataFromClient.Contains('#'))
                    {
                        if (_clientname == Program.clientNames[0])
                        {
                            Program.UpdateAnswer(dataFromClient, 1);
                        }
                        else if (_clientname == Program.clientNames[1])
                        {
                            Program.UpdateAnswer(dataFromClient, 2);
                        }
                    } else // It is a text or the score
                    {
                        if (_clientname == Program.clientNames[0])
                        {
                            Program.Broadcast(dataFromClient, _clientname, true, true, 1);
                        }
                        else if (_clientname == Program.clientNames[1])
                        {
                            Program.Broadcast(dataFromClient, _clientname, true, true, 2);
                        }
                        else
                        {
                            Program.Broadcast(dataFromClient, _clientname, true, false, 0);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }






                //try
                //{
                //    string answerFromClient = _client.ReadAnswer();
                //    if (_clientname == Program.clientNames[0])
                //    {
                //        Program.UpdateAnswer(answerFromClient, 1);
                //    }
                //    else if (_clientname == Program.clientNames[1])
                //    {
                //        Program.UpdateAnswer(answerFromClient, 2);
                //    }
                   
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.ToString());
                //}


            }
        }
    }
}
