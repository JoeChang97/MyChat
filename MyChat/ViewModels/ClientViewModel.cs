using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyChat.ViewModels
{
    class ClientViewModel : INotifyPropertyChanged
    {
        private readonly Client _client;

        

        public int Score1
        {
            get { return _client.Score1; }
            set { _client.Score1 = value; NotifyPropertyChanged(); }
        }

        
        public int Score2
        {
            get { return _client.Score2; }
            set { _client.Score2 = value; NotifyPropertyChanged(); }
        }


        public string Message
        {
            get { return _client.Message; }
            set { _client.Message = value; NotifyPropertyChanged(); }
        }

         public string Chatboard
        {
            get { return _client.Chatboard; }
            set { _client.Chatboard = value; NotifyPropertyChanged(); }
        }
        public DelegateCommand Connect { get; set; }
        public DelegateCommand Send { get; set; }
        public DelegateCommand WriteScore { get; set; }

        

        public ClientViewModel()
        {
            _client = new Client();
            _client.PropertyChanged += ClientModelChanged;

            Connect = new DelegateCommand(a=>_client.Connect(), b=>!_client.Connected);
            Send = new DelegateCommand(a => _client.Send(), b => _client.Connected);
            WriteScore = new DelegateCommand(a => _client.WriteScore(), b => _client.Connected);


        }

        private void ClientModelChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Connected"))
            {
                NotifyPropertyChanged("Connected");
                Connect.RaiseCanExecuteChanged();
                Send.RaiseCanExecuteChanged();
            }
            else if (e.PropertyName.Equals("Chatboard"))
            {
                NotifyPropertyChanged("Chatboard");
            }
            else if (e.PropertyName.Equals("Score1"))
            {
                NotifyPropertyChanged("Score1");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string prop = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
        
       








        
    
}
