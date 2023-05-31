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
            md.test();


        }

        private void CloseProgram(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void StartQuiz(object sender, RoutedEventArgs e)
        {
            Window controlsPage = new Pages.controls();
            controlsPage.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window quizplay = new QuizPlay();
            quizplay.Show();
        }
    }
}
