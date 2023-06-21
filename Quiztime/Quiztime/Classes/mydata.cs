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
        public static Int32 SelectedQuestion;
        public static bool QuizActive = false;
        public static Int32 QuizMode;
        private MySqlConnection Conn;
        private MySqlConnection Conn2;
        private MySqlConnection Conn3;
        public mydata()
        {
            string myConnectionString = "server=localhost;uid=root;" +
                "pwd=;database=quiztimedb";
            try
            {
                Conn = new MySqlConnection();
                Conn.ConnectionString = myConnectionString;
                Conn.Open();
                Conn2 = new MySqlConnection();
                Conn2.ConnectionString = myConnectionString;
                Conn2.Open();
                Conn3 = new MySqlConnection();
                Conn3.ConnectionString = myConnectionString;
                Conn3.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetDBData()
        {
            string SQL = @"SELECT * FROM `quiz`";
            MySqlCommand CMD = new MySqlCommand(SQL, Conn);
            MySqlDataReader Reader = CMD.ExecuteReader();
            List<ExcistingQuiz> quizez = ExcistingQuiz.excistingQuizzes;
            while (Reader.Read())
            {
                ExcistingQuiz quiz = new ExcistingQuiz();
                quiz.QuizQuestions = new List<QuizQuestions>();
                quiz.DBId = (int)Reader["IdQuiz"];
                quiz.LocalId = quizez.Count + 1;
                quiz.Updated = (DateTime)Reader["Updated"];
                quiz.Picture = (string)Reader["Picture"];
                quiz.QuizName = (string)Reader["QuizName"];

                string SQL2 = @"SELECT * FROM `questions` WHERE `Quiz_IdQuiz` = @IdQuiz";
                MySqlCommand CMD2 = new MySqlCommand(SQL2, Conn2);
                CMD2.Parameters.Add("@IdQuiz", MySqlDbType.VarChar).Value = (int)Reader["IdQuiz"];
                MySqlDataReader Reader2 = CMD2.ExecuteReader();
                while (Reader2.Read())
                {
                    QuizQuestions question = new QuizQuestions();
                    question.Question = (string)Reader2["Question"];
                    question.Picture = (string)Reader2["Picture"];
                    question.Timer = (int)Reader2["Timer"];
                    question.QuizAnswers = new List<QuizAnswers>();
                    string SQL3 = @"SELECT * FROM `answers` WHERE `Questions_IdQuestions` = @IdQuestion AND `Questions_Quiz_IdQuiz` = @IdQuiz";
                    MySqlCommand cmd3 = new MySqlCommand(SQL3, Conn3);
                    cmd3.Parameters.Add("@IdQuestion", MySqlDbType.VarChar).Value = (int)Reader2["IdQuestions"];
                    cmd3.Parameters.Add("@IdQuiz", MySqlDbType.VarChar).Value = (int)Reader["IdQuiz"];
                    MySqlDataReader Reader3 = cmd3.ExecuteReader();
                    while (Reader3.Read())
                    {
                        QuizAnswers quizAnswers = new QuizAnswers();
                        quizAnswers.LocalId = question.QuizAnswers.Count + 1;
                        quizAnswers.Answer = (string)Reader3["Answer"];
                        quizAnswers.Correct = Reader3.GetInt16(Reader3.GetOrdinal("Correct"));
                        question.QuizAnswers.Add(quizAnswers);
                    }
                    Reader3.Close();
                    quiz.QuizQuestions.Add(question);
                }
                Reader2.Close();
                ExcistingQuiz.excistingQuizzes.Add(quiz);
            }
            Reader.Close();
        }

        public void Test()
        {
            string sql = @"select * from quiz";
            MySqlCommand cmd = new MySqlCommand(sql, Conn);
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

        public void SaveNewQuiz()
        {
            string sql = @"INSERT INTO `quiz`(`QuizName`, `Picture`) VALUES (@Name, @Picture);";
            MySqlCommand cmd = new MySqlCommand(sql, Conn);
            cmd.Parameters.Add("@Name", MySqlDbType.VarChar).Value = NewQuizName;
            if (CoverPicture == null)
            {
                cmd.Parameters.Add("@Picture", MySqlDbType.VarChar).Value = "";
            }
            else
            {
                cmd.Parameters.Add("@Picture", MySqlDbType.VarChar).Value = "/Media/" + CoverPicture;
            }
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.ExecuteNonQuery();
            string sql2 = @"SELECT IdQuiz from quiz WHERE QuizName = @Name";
            MySqlCommand cmd2 = new MySqlCommand(sql2, Conn);
            cmd2.Parameters.Add("@Name", MySqlDbType.VarChar).Value = NewQuizName;
            MySqlDataReader Reader = cmd2.ExecuteReader();
            int SQLQuizId = 0;
            int SQLQuestionId = 0;
            while (Reader.Read())
            {
                SQLQuizId = (int)Reader["IdQuiz"];
            }
            Reader.Close();

            foreach (var item in mydata.NewQuizQuestion)
            {
                string sql3 = @"INSERT INTO `questions`(`Question`, `Picture`, `Timer`, `Quiz_IdQuiz`) VALUES (@Name, @Picture, @Timer, @Id);";
                MySqlCommand cmd3 = new MySqlCommand(sql3, Conn);
                if (item.Picture == null)
                {
                    cmd3.Parameters.Add("@Name", MySqlDbType.VarChar).Value = "";
                }
                else
                {
                    cmd3.Parameters.Add("@Name", MySqlDbType.VarChar).Value = item.Question;
                }
                cmd3.Parameters.Add("@Picture", MySqlDbType.VarChar).Value = "/Media/" + item.Picture;
                cmd3.Parameters.Add("@Timer", MySqlDbType.VarChar).Value = item.Timer;
                cmd3.Parameters.Add("@Id", MySqlDbType.VarChar).Value = SQLQuizId;
                cmd3.CommandType = System.Data.CommandType.Text;
                cmd3.ExecuteNonQuery();

                string sql4 = @"SELECT `IdQuestions` FROM `questions` WHERE `question` = @Question AND `Quiz_IdQuiz` = @QuizId";
                MySqlCommand cmd4 = new MySqlCommand(sql4, Conn);
                cmd4.Parameters.Add("@Question", MySqlDbType.VarChar).Value = item.Question;
                cmd4.Parameters.Add("@QuizId", MySqlDbType.VarChar).Value = SQLQuizId;
                MySqlDataReader Reader2 = cmd4.ExecuteReader();
                while (Reader2.Read())
                {
                    SQLQuestionId = (int)Reader2["IdQuestions"];
                }
                Reader2.Close();
                string sql5 = @"INSERT INTO `answers`(`Answer`, `Correct`, `Questions_IdQuestions`, `Questions_Quiz_IdQuiz`) VALUES (@Answer, @Correct, @QuestionId, @QuizId)";
                MySqlCommand cmd5 = new MySqlCommand(sql5, Conn);
                if (item.Amount >= 1)
                {
                    cmd5.Parameters.Add("@Answer", MySqlDbType.VarChar).Value = item.Answer1;
                    if (item.CAnswer == 1)
                    {
                        cmd5.Parameters.Add("@Correct", MySqlDbType.VarChar).Value = 1;
                    }
                    else
                    {
                        cmd5.Parameters.Add("@Correct", MySqlDbType.VarChar).Value = 0;
                    }
                    cmd5.Parameters.Add("@QuestionId", MySqlDbType.VarChar).Value = SQLQuestionId;
                    cmd5.Parameters.Add("@QuizId", MySqlDbType.VarChar).Value = SQLQuizId;
                    cmd5.CommandType = System.Data.CommandType.Text;
                    cmd5.ExecuteNonQuery();
                    cmd5.Parameters.Clear();
                }
                if (item.Amount >= 2)
                {
                    cmd5.Parameters.Add("@Answer", MySqlDbType.VarChar).Value = item.Answer2;
                    if (item.CAnswer == 2)
                    {
                        cmd5.Parameters.Add("@Correct", MySqlDbType.VarChar).Value = 1;
                    }
                    else
                    {
                        cmd5.Parameters.Add("@Correct", MySqlDbType.VarChar).Value = 0;
                    }
                    cmd5.Parameters.Add("@QuestionId", MySqlDbType.VarChar).Value = SQLQuestionId;
                    cmd5.Parameters.Add("@QuizId", MySqlDbType.VarChar).Value = SQLQuizId;
                    cmd5.CommandType = System.Data.CommandType.Text;
                    cmd5.ExecuteNonQuery();
                    cmd5.Parameters.Clear();
                }
                if (item.Amount >= 3)
                {
                    cmd5.Parameters.Add("@Answer", MySqlDbType.VarChar).Value = item.Answer3;
                    if (item.CAnswer == 3)
                    {
                        cmd5.Parameters.Add("@Correct", MySqlDbType.VarChar).Value = 1;
                    }
                    else
                    {
                        cmd5.Parameters.Add("@Correct", MySqlDbType.VarChar).Value = 0;
                    }
                    cmd5.Parameters.Add("@QuestionId", MySqlDbType.VarChar).Value = SQLQuestionId;
                    cmd5.Parameters.Add("@QuizId", MySqlDbType.VarChar).Value = SQLQuizId;
                    cmd5.CommandType = System.Data.CommandType.Text;
                    cmd5.ExecuteNonQuery();
                    cmd5.Parameters.Clear();
                }
                if (item.Amount >= 4)
                {
                    cmd5.Parameters.Add("@Answer", MySqlDbType.VarChar).Value = item.Answer4;
                    if (item.CAnswer == 4)
                    {
                        cmd5.Parameters.Add("@Correct", MySqlDbType.VarChar).Value = 1;
                    }
                    else
                    {
                        cmd5.Parameters.Add("@Correct", MySqlDbType.VarChar).Value = 0;
                    }
                    cmd5.Parameters.Add("@QuestionId", MySqlDbType.VarChar).Value = SQLQuestionId;
                    cmd5.Parameters.Add("@QuizId", MySqlDbType.VarChar).Value = SQLQuizId;
                    cmd5.CommandType = System.Data.CommandType.Text;
                    cmd5.ExecuteNonQuery();
                    cmd5.Parameters.Clear();
                }
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
