using Quiztime.Classes;
using Quiztime.Pages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace Quiztime
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mydata md = new mydata();
            md.GetDBData();
            QuizGrid.ItemsSource = ExcistingQuiz.excistingQuizzes;
        }

         private void OpenQuiz(object sender, RoutedEventArgs e)
        {
            mydata.QuizMode = 3;
            mydata.SelectedQuiz = (int)((Button)sender).Tag;
            mydata.SelectedQuestion = 1;
            Window QuizPlayControl = new Controls();
            QuizPlayControl.Show();
            
        }

        private void CheckQuiz(object sender, RoutedEventArgs e)
        {
            mydata.QuizMode = 2;
            mydata.SelectedQuiz = (int)((Button)sender).Tag;
            Window QuizPlay = new QuizPlay();
            QuizPlay.Show();
        }

        private void EditQuiz(object sender, RoutedEventArgs e)
        {
            mydata.QuizMode = 1;
            mydata.SelectedQuiz = (int)((Button)sender).Tag;
            Window QuizPlay = new QuizPlay();
            QuizPlay.Show();
        }

        private void OpenAddQuiz(object sender, RoutedEventArgs e)
        {
            mydata.QuizMode = 0;
            Window AddQuiz = new QuizCreate();
            AddQuiz.Show();
        }
    }
}
