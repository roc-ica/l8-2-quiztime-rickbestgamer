using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiztime.Classes
{
    public class cQuiz
    {
        public cQuiz() { }

        public Int32 Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public DateTime Updated { get; set; }
    }

    public class NewQuizQuestions
    {
        public NewQuizQuestions() { }

        public int Id { get; set; }
        public string Question { get; set; }
        public string Picture { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
        public int CAnswer { get; set; }
    }
}
