using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyChat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<int, String> questions = new Dictionary<int, String>();
        private Dictionary<int, bool> answers = new Dictionary<int, bool>();
        private int questionNum = 0;
        private string playing = "Player 1";

        public MainWindow()
        {
            addQuestions();
            UpdateUI();
            InitializeComponent();
        }

        private void addQuestions()
        {
            questions.Add(0, "Are you a robot?");
            answers.Add(0, false);
        }

        private void TrueBtn_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("True button clicked");
            CheckAnswerFor(true);
        }

        private void FalseBtn_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("False button clicked");
            CheckAnswerFor(false);
        }

        private void UpdateUI()
        {
            QuestionLabel.Content = questions[questionNum];
        }

        private void CheckAnswerFor(bool answer)
        {
            if (answer == (bool)answers[questionNum]) {
                Console.WriteLine(playing + " got the right answer");
            } else
            {
                Console.WriteLine(playing + " got the wrong answer");
            }
            questionNum++;
        }
    }
}
