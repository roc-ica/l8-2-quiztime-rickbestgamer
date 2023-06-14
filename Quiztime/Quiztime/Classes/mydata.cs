using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace Quiztime.Classes
{
    public class mydata
    {
        public static List<cQuiz> Quiz = new List<cQuiz>();
        public static List<NewQuizQuestions> NewQuizQuestion = new List<NewQuizQuestions>();
        public static Int32 SelectedQuiz;
        private MySqlConnection Conn;
        public mydata()
        {
            string myConnectionString = "server=localhost;uid=root;" +
                "pwd=;database=quiztimedb";

            try
            {
                Conn = new MySqlConnection();
                Conn.ConnectionString = myConnectionString;
                Conn.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Test()
        {
            string sql = @"select * from quiz";
            MySqlCommand cmd = new MySqlCommand(sql, Conn);
            //cmd.Parameters.Add("@name",MySqlDbType.VarChar).Value = name;
            //cmd.Parameters.Add("@picture", MySqlType.VarChar).Value = surname;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Quiz.Add(new cQuiz());
                Quiz[Quiz.Count - 1].Id = (Int32)reader["idQuiz"];
                Quiz[Quiz.Count - 1].Name = (string)reader["QuizName"];
                Quiz[Quiz.Count - 1].Picture = (string)reader["Picture"];
                Quiz[Quiz.Count - 1].Updated = (DateTime)reader["Updated"];
            }
        }

        public void NewQuestion()
        {
            NewQuizQuestion.Add(new NewQuizQuestions());
            NewQuizQuestion[NewQuizQuestion.Count - 1].Id = NewQuizQuestion.Count;
        }
    }
}
