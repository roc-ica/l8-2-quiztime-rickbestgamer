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
    /// Interaction logic for QuizSelect.xaml
    /// </summary>
    public partial class QuizSelect : Window
    {
        public QuizSelect()
        {
            InitializeComponent();
            QuizGrid.ItemsSource = mydata.Quiz;
        }

        private void OpenQuiz(object sender, RoutedEventArgs e)
        {
            mydata.SelectedQuiz = (int)((Button)sender).Tag;
            Window QuizPlay = new QuizPlay();
            QuizPlay.Show();
        }

        private void CheckQuiz(object sender, RoutedEventArgs e)
        {
            mydata.SelectedQuiz = (int)((Button)sender).Tag;
            Window QuizPlay = new QuizPlay();
            QuizPlay.Show();
        }

        private void EditQuiz(object sender, RoutedEventArgs e)
        {
            mydata.SelectedQuiz = (int)((Button)sender).Tag;
            Window QuizPlay = new QuizPlay();
            QuizPlay.Show();
        }
    }
}
