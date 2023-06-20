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
        public int Amount { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
        public int CAnswer { get; set; }
        public int Timer { get; set; }
    }

    public class ExcistingQuiz
    {
        public static List<ExcistingQuiz> excistingQuizzes = new List<ExcistingQuiz>();
        public ExcistingQuiz() { }
        public int DBId { get; set; }
        public int LocalId { get; set; }
        public string QuizName { get; set; }
        public string Picture { get; set; }
        public DateTime Updated { get; set; }
        public virtual List<QuizQuestions> QuizQuestions{ get; set; }
    }
    public class QuizQuestions
    {
        public QuizQuestions() { }

        public string Question { get; set; }
        public string Picture { get; set; }
        public int Timer { get; set; }

        public virtual List<QuizAnswers> QuizAnswers { get; set; }
    }

    public class QuizAnswers
    {
        public QuizAnswers() { }
        public string Answer { get; set; }
        public int Correct { get; set; }
    }
    public class Box
    {
        public Box() { }

        public int BoxId { get; set; }
        public string BoxName { get; set; }

        public virtual List<Item> Item { get; set; }
    }

    public class Item
    {
        public Item()
        {
        }
        public int ItemId { get; set; }
        public string Description { get; set; }

    }
}
