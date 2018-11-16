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
    class Client : INotifyPropertyChanged
    {
        private TcpClient _client;

        private int _score1;

        public int Score1
        {
            get { return _score1; }
            set { _score1 = value; OnPropertyChanged(); }
        }

        private int _score2;

        public int Score2
        {
            get { return _score2; }
            set { _score2 = value; OnPropertyChanged(); }
        }

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
            ((MainWindow)System.Windows.Application.Current.MainWindow).TrueBtn.IsEnabled = true;
            ((MainWindow)System.Windows.Application.Current.MainWindow).FalseBtn.IsEnabled = true;

            Send();
            _chatboard = "Welcome " + _message;
            var thread = new Thread(UpdateUniverse);
            thread.Start();
        }

        private void UpdateUniverse()
        {
            while (true)
            {
                string message = _client.ReadString();
                if (message[message.Length - 1] == '-')
                {
                    // the player pressed True/False button
                    Score1 = Int32.Parse(message.Substring(message.Length - 2, 1));
                } else
                {
                    Chatboard += "\r\n" + message;
                }
                
            }


        }

        public void Send()
        {
            _client.WriteString(_message, false);
        }

        public void WriteScore()
        {
            _client.WriteString(_score1.ToString(), true);
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
