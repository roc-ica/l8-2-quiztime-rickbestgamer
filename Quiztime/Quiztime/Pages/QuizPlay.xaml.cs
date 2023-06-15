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
            InitializeComponent();
            if (mydata.SelectedQuiz != 0)
            {
                foreach (var item in mydata.Quiz)
                {
                    if (item.Id == mydata.SelectedQuiz)
                    {
                        QuizTitle.Text = item.Name;
                    }
                }
            }

            StartTimer();
            StartCounter();

        }

        readonly int TimeDelay = 10;

        private async void StartTimer()
        {
            while (TimerProgressbar.EndAngle > 0)
            {
                await Task.Delay(1000);
                MovePRSB();

            }

        }

        private async void MovePRSB()
        {
            for (int i = 0; i < 360 / TimeDelay; i++)
            {
                if (TimerProgressbar.EndAngle > 0)
                {
                    TimerProgressbar.EndAngle--;
                    await Task.Delay(1);
                }
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
