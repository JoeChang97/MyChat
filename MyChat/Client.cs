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
        private Dictionary<int, String> questions = new Dictionary<int, String>();
        private Dictionary<int, bool> answers = new Dictionary<int, bool>();
        private int questionNum = 0;
        private string _currentQuestion;
        private TcpClient _client;

        public string CurrentQuestion
        {
            get { return _currentQuestion; }
            set { _currentQuestion = value; OnPropertyChanged(); }
        }

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
            addQuestions();
            _currentQuestion = questions[questionNum];
            _score1 = 0;
            _score2 = 0;
            UpdateUI();

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
                if (message.Contains('#'))
                    // It is just checking the answer
                    break;
                if (message[message.Length - 1] == '-')
                {   // It is the first player playing
                    if (int.TryParse(message.Substring(0, message.IndexOf("--", StringComparison.Ordinal)), out int score))
                    {
                        // It is a score
                        Score1 = score;
                    } else
                    {
                        // It is a text
                        Chatboard += "\r\n" + message.Substring(0, message.IndexOf("--", StringComparison.Ordinal));
                    }
                    
                }
                else if (message[message.Length - 1] == '+')
                {   // It is the second player playing
                    if (int.TryParse(message.Substring(0, message.IndexOf("++", StringComparison.Ordinal)), out int score))
                    {
                        // It is a score
                        Score2 = score;
                    }
                    else
                    {
                        // It is a text
                        Chatboard += "\r\n" + message.Substring(0, message.IndexOf("++", StringComparison.Ordinal));
                    }
                } else 
                {
                    // It is the first connect
                    Chatboard += "\r\n" + message;
                }
            }
        }

        public void Send()
        {
            _client.WriteString(_message, false, 0);
        }

        private void addQuestions()
        {
            questions.Add(0, "Are you a robot?");
            answers.Add(0, false);

            questions.Add(1, "The Capital of NY is NYC");
            answers.Add(1, false);

            questions.Add(2, "SAU was founded in 1892");
            answers.Add(2, true);

            questions.Add(3, "It is Nevada law that all casinos" + "\n" + " have one clock on every floor");
            answers.Add(3, false);

            questions.Add(4, "George Washington had wooden teeth");
            answers.Add(4, false);

            questions.Add(5, "An American was the first man in space.");
            answers.Add(5, false);

            questions.Add(6, "Are you a robot?");
            answers.Add(6, false);

            questions.Add(7, "Are you a robot?");
            answers.Add(7, false);

            questions.Add(8, "Are you a robot?");
            answers.Add(8, false);

            questions.Add(9, "Are you a robot?");
            answers.Add(9, false);
        }
        public void WriteTrue()
        {
            if (questionNum == 10)
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).TrueBtn.IsEnabled = false;
                ((MainWindow)System.Windows.Application.Current.MainWindow).FalseBtn.IsEnabled = false;
            }
            if (answers[questionNum - 1] == true)
            {
                _client.WriteAnswer("correct#");
            }
            else { _client.WriteAnswer("incorrect#"); }

            UpdateUI();

        }
        public void WriteFalse()
        {
            if (questionNum == 10)
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).TrueBtn.IsEnabled = false;
                ((MainWindow)System.Windows.Application.Current.MainWindow).FalseBtn.IsEnabled = false;
            }
            if (answers[questionNum - 1] == false)
            {
                _client.WriteAnswer("correct#");
            }
            else { _client.WriteAnswer("incorrect#"); }

            UpdateUI();
        }

        public void UpdateUI()
        {
            
            questionNum++;
            if (questionNum == 11) {
                questionNum = 10;
            }
            ((MainWindow)System.Windows.Application.Current.MainWindow).QuestionLabel.Content = questions[questionNum - 1];
            ((MainWindow)System.Windows.Application.Current.MainWindow).GameStatus.Content = "Question " + questionNum + " of 10";
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
