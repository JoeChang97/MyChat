using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyChat
{
    class Client:INotifyPropertyChanged
    {
        private TcpClient _client;

        private string _chatboard;

        public string Chatboard
        {
            get { return _chatboard; }
            set { _chatboard = value; OnPropertyChanged(); }
        }


        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(); }
        }

        public bool Connected
        {
            get { return _client != null && _client.Connected; }
        }

        public void Connect()
        {
            _client = new TcpClient("127.0.0.1", 8888);
            OnPropertyChanged("Connected");
            Send();
            _chatboard = "Welcome" + _message;
            var thread = new Thread(GetMessage);
            thread.Start();
        }

        private void GetMessage()
        {
            while (true)
            {
                string message = _client.ReadString();
                Chatboard += "\r\n" + message;
            }


        }

        public void Send()
        {
            _client.WriteString(_message);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
