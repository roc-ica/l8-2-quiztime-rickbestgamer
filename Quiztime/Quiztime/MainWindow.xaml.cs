using Quiztime.Classes;
using Quiztime.Pages;
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
            md.Test();
            QuizGrid.ItemsSource = mydata.Quiz;
            Console.WriteLine("list;");
            Console.WriteLine(mydata.Quiz.ToString());


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

        private void OpenAddQuiz(object sender, RoutedEventArgs e)
        {
            Window AddQuiz = new QuizCreate();
            AddQuiz.Show();
        }
    }
}
