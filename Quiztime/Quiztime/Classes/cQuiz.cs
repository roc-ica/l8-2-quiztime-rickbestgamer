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

        public string name { get; set; }
        public string picture { get; set; }
    }
    public class Box
    {
        public Box() { }

        public int BoxId { get; set; }
        public string BoxName { get; set; }

        public virtual Item Item { get; set; }
    }

    public class Item
    {
        public Item()
        {
            Boxes = new List<Box>();
        }
        public int ItemId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Box> Boxes { get; set; }
    }
}
