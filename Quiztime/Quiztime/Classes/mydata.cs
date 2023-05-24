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
        private MySqlConnection conn;
        public mydata() {
            string myConnectionString = "server=localhost;uid=quiztimeuser;" +
                "pwd=quiztime;database=quiztime";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void test()
        {
            string sql = @"select * from quiz";
            MySqlCommand cmd = new MySqlCommand(sql,conn);
            //cmd.Parameters.Add("@name",MySqlDbType.VarChar).Value = name;
            //cmd.Parameters.Add("@picture", MySqlType.VarChar).Value = surname;
            MySqlDataReader reader = cmd.ExecuteReader();
            var cquiz = new cQuiz();
            while(reader.Read())
            {
                cquiz.name = (string)reader["name"];
            }
            while(cquiz.name.)
            Console.WriteLine(cquiz.name);
        }
    }
}
