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
            CreateDB();
            InitializeComponent();
            mydata md = new mydata();
            md.GetDBData();
            QuizGrid.ItemsSource = ExcistingQuiz.excistingQuizzes;
            
        }

        static void CreateDB()
        {
            string connectionString = "Data Source=localhost;Integrated Security=SSPI;database=master;";
            string cmdText = "create database MYTESTDB";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            if (sqlConnection.State != ConnectionState.Open)
            {
                try
                {
                    Console.WriteLine("connection is closed");
                    sqlConnection.Open();
                    Console.WriteLine("connection is open");
                    SqlCommand sqlCommand = new SqlCommand(cmdText);
                    sqlCommand.ExecuteNonQuery();
                    Console.WriteLine("database created");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                sqlConnection.Close();
                }
            }
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
