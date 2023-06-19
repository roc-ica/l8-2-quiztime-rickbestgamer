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
        public static string NewQuizName;
        public static string CoverPicture;
        public static List<cQuiz> Quiz = new List<cQuiz>();
        public static List<NewQuizQuestions> NewQuizQuestion = new List<NewQuizQuestions>();
        public static Int32 SelectedQuiz;
        public static Int32 QuizMode;
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

        public void SaveQuiz()
        {
            string sql = @"INSERT INTO `quiz`(`QuizName`, `Picture`) VALUES (@Name, @Picture);";
            MySqlCommand cmd = new MySqlCommand(sql, Conn);
            cmd.Parameters.Add("@Name", MySqlDbType.VarChar).Value = NewQuizName;
            cmd.Parameters.Add("@Picture", MySqlDbType.VarChar).Value = CoverPicture;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.ExecuteNonQuery();
            string sql2 = @"SELECT IdQuiz from quiz WHERE QuizName = @Name";
            MySqlCommand cmd2 = new MySqlCommand(sql2, Conn);
            cmd2.Parameters.Add("@Name", MySqlDbType.VarChar).Value = NewQuizName;
            MySqlDataReader Reader = cmd2.ExecuteReader();
            int sqlid = 0;
            while (Reader.Read())
            {
                sqlid = (int)Reader["IdQuiz"];
            }
            Reader.Close();

            foreach (var item in mydata.NewQuizQuestion)
            {
                string sql3 = @"INSERT INTO `questions`(`Question`, `Picture`, `Timer`, `Quiz_IdQuiz`) VALUES (@Name, @Picture, @Timer, @Id);";
                MySqlCommand cmd3 = new MySqlCommand(sql3, Conn);
                cmd3.Parameters.Add("@Name", MySqlDbType.VarChar).Value = item.Question;
                cmd3.Parameters.Add("@Picture", MySqlDbType.VarChar).Value = item.Picture;
                cmd3.Parameters.Add("@Timer", MySqlDbType.VarChar).Value = item.Timer;
                cmd3.Parameters.Add("@Id", MySqlDbType.VarChar).Value = sqlid;
                cmd3.CommandType = System.Data.CommandType.Text;
                cmd3.ExecuteNonQuery();
            }
            NewQuizName = null;
            CoverPicture = null;
            NewQuizQuestion = new List<NewQuizQuestions>();
        }

        public void NewQuestion()
        {
            NewQuizQuestion.Add(new NewQuizQuestions());
            NewQuizQuestion[NewQuizQuestion.Count - 1].Id = NewQuizQuestion.Count;
        }

        public bool QuizNameExcist()
        {
            string SQLQ = @"SELECT * FROM `quiz` WHERE `QuizName` = @QuizName";
            MySqlCommand cmd = new MySqlCommand(@SQLQ, Conn);
            cmd.Parameters.Add("@QuizName", MySqlDbType.VarChar).Value = NewQuizName;
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Close();
                return false;
            }
            else
            {
                reader.Close();
                return true;
            }
        }
    }
}
