using Quiztime.Classes;
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
using System.Windows.Shapes;

namespace Quiztime.Pages
{
    /// <summary>
    /// Interaction logic for QuizPlay.xaml
    /// </summary>
    public partial class QuizPlay : Window
    {
        public QuizPlay()
        {
            List<string> list = new List<string>();
            InitializeComponent();
            if (mydata.SelectedQuiz != 0)
            {
                foreach (var item in mydata.Quiz)
                {
                    if (item.Id == mydata.SelectedQuiz)
                    {
                        QuizTitle.Text = item.Name;
                        list.Add(item.Picture);
                    }
                }
            }
            StartCounter();
            MovePRSB();
        }

        readonly int TimeDelay = 4;

        private async void MovePRSB()
        {
            double test = (double)360 / 1000 * 100 / TimeDelay;
            Console.WriteLine(test);
            while (TimerProgressbar.EndAngle > 0)
            {
                await Task.Delay(100);
                TimerProgressbar.EndAngle -= test;
            }
        }

        private async void StartCounter()
        {
            int DelayText = TimeDelay;
            TimerText.Text = "" + DelayText;
            while (TimerText.Text != "0")
            {
                await Task.Delay(1000);
                DelayText--;
                TimerText.Text = "" + DelayText;
            }
        }
    }
}
