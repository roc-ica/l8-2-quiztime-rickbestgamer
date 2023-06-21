using Quiztime.Classes;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Quiztime.Pages
{
    /// <summary>
    /// Interaction logic for controls.xaml
    /// </summary>
    public partial class Controls : Window
    {
        QuizPlay quizPlay = new QuizPlay();
        public Controls()
        {
            InitializeComponent();
            quizPlay.Show();
        }

        private void NextQuestion(object sender, RoutedEventArgs e)
        {
            if (mydata.SelectedQuestion > 0 && mydata.QuizActive == false && ExcistingQuiz.excistingQuizzes[mydata.SelectedQuiz - 1].QuizQuestions.Count - 1 >= mydata.SelectedQuestion)
            {
                mydata.SelectedQuestion++;
                mydata.QuizActive = true;
                quizPlay.NextQuestion();
            }
        }

        private void PreviousQuestion(object sender, RoutedEventArgs e)
        {
            if (mydata.SelectedQuestion > 1 && mydata.QuizActive == false)
            {
                mydata.SelectedQuestion--;
                mydata.QuizActive = true;
                quizPlay.NextQuestion();
            }
        }
    }
}
