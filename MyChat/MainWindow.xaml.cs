using MyChat.ViewModels;
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
        private bool player1IsPlaying = true;
        private string playing1 = "Player 1 is playing";
        private string playing2 = "Player 2 is playing";
        ClientViewModel vm = new ClientViewModel();

        public MainWindow()
        {
            addQuestions();
            
            InitializeComponent();
            UpdateUI();
            this.DataContext = vm;
            vm.Score1 = 0;
            vm.Score2 = 0;
        }

        private void addQuestions()
        {
            questions.Add(0, "Are you a robot?");
            answers.Add(0, false);

            questions.Add(1, "The Capital of NY is NYC");
            answers.Add(1, false);

            questions.Add(2, "SAU was founded in 1892");
            answers.Add(2, true);

            questions.Add(3, "It is Nevada law that all casinos" +"\n"+" have one clock on every floor");
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

        private void TrueBtn_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("True button clicked");
            CheckAnswerFor(true);
            UpdateUI();
            
        }

        private void FalseBtn_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("False button clicked");
            CheckAnswerFor(false);
            UpdateUI();
        }

        private void UpdateUI()
        {   
            QuestionLabel.Content = questions[questionNum];
            Console.WriteLine("question: " + questionNum);
           
            PlayerTurn.Content = player1IsPlaying ? playing1 : playing2;
            Console.WriteLine(PlayerTurn.Content);
        }

        private void CheckAnswerFor(bool answer)
        {
            if (answer == (bool)answers[questionNum]) {
                Console.WriteLine(playing1 + " got the right answer");
                gameImg.Source = (ImageSource)FindResource("ImageCorrect");
                if (player1IsPlaying)
                {
                    vm.Score1 += 1;
                } else
                {
                    vm.Score2 += 1;
                }
                // TODO: show RIGHT ANSWER meme image
            } else
            {
                Console.WriteLine(playing1 + " got the wrong answer");
                gameImg.Source = (ImageSource)FindResource("ImageIncorrect");
                // TODO: show WRONG ANSWER meme image
            }
            questionNum = questionNum == questions.Count - 1 ? 0 : questionNum + 1;
            player1IsPlaying = !player1IsPlaying;
        }
    }
}
