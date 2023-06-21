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
        int TimeDelay;
        public QuizPlay()
        {
            InitializeComponent();
            if (mydata.SelectedQuiz != 0)
            {
                foreach (var item in ExcistingQuiz.excistingQuizzes)
                {
                    if (item.LocalId == mydata.SelectedQuiz)
                    {
                        QuizTitle.Text = item.QuizName;
                        TitleQuestion.Text = item.QuizQuestions[mydata.SelectedQuestion - 1].Question;
                        if (item.QuizQuestions[mydata.SelectedQuestion - 1].QuizAnswers.Count == 4)
                        {
                            Grid.SetRow(BorderA1, 1);
                            Grid.SetRowSpan(BorderA1, 2);
                            Grid.SetColumn(BorderA1, 1);
                            Grid.SetColumnSpan(BorderA1, 2);
                            BorderA1.Visibility = Visibility.Visible;
                            Grid.SetRow(BorderA2, 1);
                            Grid.SetRowSpan(BorderA2, 2);
                            Grid.SetColumn(BorderA2, 4);
                            Grid.SetColumnSpan(BorderA2, 2);
                            BorderA2.Visibility = Visibility.Visible;
                            Grid.SetRow(BorderA3, 3);
                            Grid.SetRowSpan(BorderA3, 2);
                            Grid.SetColumn(BorderA3, 1);
                            Grid.SetColumnSpan(BorderA3, 2);
                            BorderA3.Visibility = Visibility.Visible;
                            Grid.SetRow(BorderA4, 3);
                            Grid.SetRowSpan(BorderA4, 2);
                            Grid.SetColumn(BorderA4, 4);
                            Grid.SetColumnSpan(BorderA4, 2);
                            BorderA4.Visibility = Visibility.Visible;
                        }
                        else if (item.QuizQuestions[mydata.SelectedQuestion - 1].QuizAnswers.Count == 3)
                        {
                            Grid.SetRow(BorderA1, 1);
                            Grid.SetRowSpan(BorderA1, 2);
                            Grid.SetColumn(BorderA1, 1);
                            Grid.SetColumnSpan(BorderA1, 2);
                            BorderA1.Visibility = Visibility.Visible;
                            Grid.SetRow(BorderA2, 1);
                            Grid.SetRowSpan(BorderA2, 2);
                            Grid.SetColumn(BorderA2, 4);
                            Grid.SetColumnSpan(BorderA2, 2);
                            BorderA2.Visibility = Visibility.Visible;
                            Grid.SetRow(BorderA3, 3);
                            Grid.SetRowSpan(BorderA3, 2);
                            Grid.SetColumn(BorderA3, 1);
                            Grid.SetColumnSpan(BorderA3, 5);
                            BorderA3.Visibility = Visibility.Visible;
                            Grid.SetRow(BorderA4, 3);
                            Grid.SetRowSpan(BorderA4, 2);
                            Grid.SetColumn(BorderA4, 4);
                            Grid.SetColumnSpan(BorderA4, 2);
                            BorderA4.Visibility = Visibility.Collapsed;
                        }
                        else if (item.QuizQuestions[mydata.SelectedQuestion - 1].QuizAnswers.Count == 2)
                        {
                            Grid.SetRow(BorderA1, 1);
                            Grid.SetRowSpan(BorderA1, 5);
                            Grid.SetColumn(BorderA1, 1);
                            Grid.SetColumnSpan(BorderA1, 2);
                            BorderA1.Visibility = Visibility.Visible;
                            Grid.SetRow(BorderA2, 1);
                            Grid.SetRowSpan(BorderA2, 5);
                            Grid.SetColumn(BorderA2, 4);
                            Grid.SetColumnSpan(BorderA2, 2);
                            BorderA2.Visibility = Visibility.Visible;
                            Grid.SetRow(BorderA3, 3);
                            Grid.SetRowSpan(BorderA3, 2);
                            Grid.SetColumn(BorderA3, 1);
                            Grid.SetColumnSpan(BorderA3, 5);
                            BorderA3.Visibility = Visibility.Collapsed;
                            Grid.SetRow(BorderA4, 3);
                            Grid.SetRowSpan(BorderA4, 2);
                            Grid.SetColumn(BorderA4, 4);
                            Grid.SetColumnSpan(BorderA4, 2);
                            BorderA4.Visibility = Visibility.Collapsed;
                        }
                        foreach (var item1 in item.QuizQuestions[mydata.SelectedQuestion - 1].QuizAnswers)
                        {
                            if (item1.LocalId == 1)
                            {
                                Answer1.Text = item1.Answer;
                            }
                            if (item1.LocalId == 2)
                            {
                                Answer2.Text = item1.Answer;
                            }
                            if (item1.LocalId == 3)
                            {
                                Answer3.Text = item1.Answer;
                            }
                            if (item1.LocalId == 4)
                            {
                                Answer4.Text = item1.Answer;
                            }
                        }
                        BitmapImage image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = new Uri(item.QuizQuestions[mydata.SelectedQuestion - 1].Picture);
                        image.EndInit();
                        QuestionImage.Source = image;
                        TimeDelay = item.QuizQuestions[mydata.SelectedQuestion - 1].Timer;
                    }
                }
            }
            StartCounter();
            MovePRSB();
        }

        public void NextQuestion()
        {
            if (mydata.SelectedQuiz != 0)
            {
                foreach (var item in ExcistingQuiz.excistingQuizzes)
                {
                    if (item.LocalId == mydata.SelectedQuiz)
                    {
                        QuizTitle.Text = item.QuizName;
                        TitleQuestion.Text = item.QuizQuestions[mydata.SelectedQuestion - 1].Question;
                        if (item.QuizQuestions[mydata.SelectedQuestion - 1].QuizAnswers.Count == 4)
                        {
                            Grid.SetRow(BorderA1, 1);
                            Grid.SetRowSpan(BorderA1, 2);
                            Grid.SetColumn(BorderA1, 1);
                            Grid.SetColumnSpan(BorderA1, 2);
                            BorderA1.Visibility = Visibility.Visible;
                            Grid.SetRow(BorderA2, 1);
                            Grid.SetRowSpan(BorderA2, 2);
                            Grid.SetColumn(BorderA2, 4);
                            Grid.SetColumnSpan(BorderA2, 2);
                            BorderA2.Visibility = Visibility.Visible;
                            Grid.SetRow(BorderA3, 3);
                            Grid.SetRowSpan(BorderA3, 2);
                            Grid.SetColumn(BorderA3, 1);
                            Grid.SetColumnSpan(BorderA3, 2);
                            BorderA3.Visibility = Visibility.Visible;
                            Grid.SetRow(BorderA4, 3);
                            Grid.SetRowSpan(BorderA4, 2);
                            Grid.SetColumn(BorderA4, 4);
                            Grid.SetColumnSpan(BorderA4, 2);
                            BorderA4.Visibility = Visibility.Visible;
                        }
                        else if (item.QuizQuestions[mydata.SelectedQuestion - 1].QuizAnswers.Count == 3)
                        {
                            Grid.SetRow(BorderA1, 1);
                            Grid.SetRowSpan(BorderA1, 2);
                            Grid.SetColumn(BorderA1, 1);
                            Grid.SetColumnSpan(BorderA1, 2);
                            BorderA1.Visibility = Visibility.Visible;
                            Grid.SetRow(BorderA2, 1);
                            Grid.SetRowSpan(BorderA2, 2);
                            Grid.SetColumn(BorderA2, 4);
                            Grid.SetColumnSpan(BorderA2, 2);
                            BorderA2.Visibility = Visibility.Visible;
                            Grid.SetRow(BorderA3, 3);
                            Grid.SetRowSpan(BorderA3, 2);
                            Grid.SetColumn(BorderA3, 1);
                            Grid.SetColumnSpan(BorderA3, 5);
                            BorderA3.Visibility = Visibility.Visible;
                            Grid.SetRow(BorderA4, 3);
                            Grid.SetRowSpan(BorderA4, 2);
                            Grid.SetColumn(BorderA4, 4);
                            Grid.SetColumnSpan(BorderA4, 2);
                            BorderA4.Visibility = Visibility.Collapsed;
                        }
                        else if (item.QuizQuestions[mydata.SelectedQuestion-1].QuizAnswers.Count == 2)
                        {
                            Grid.SetRow(BorderA1, 1);
                            Grid.SetRowSpan(BorderA1, 5);
                            Grid.SetColumn(BorderA1, 1);
                            Grid.SetColumnSpan(BorderA1, 2);
                            BorderA1.Visibility = Visibility.Visible;
                            Grid.SetRow(BorderA2, 1);
                            Grid.SetRowSpan(BorderA2, 5);
                            Grid.SetColumn(BorderA2, 4);
                            Grid.SetColumnSpan(BorderA2, 2);
                            BorderA2.Visibility = Visibility.Visible;
                            Grid.SetRow(BorderA3, 3);
                            Grid.SetRowSpan(BorderA3, 2);
                            Grid.SetColumn(BorderA3, 1);
                            Grid.SetColumnSpan(BorderA3, 5);
                            BorderA3.Visibility = Visibility.Collapsed;
                            Grid.SetRow(BorderA4, 3);
                            Grid.SetRowSpan(BorderA4, 2);
                            Grid.SetColumn(BorderA4, 4);
                            Grid.SetColumnSpan(BorderA4, 2);
                            BorderA4.Visibility = Visibility.Collapsed;
                        }
                        foreach (var item1 in item.QuizQuestions[mydata.SelectedQuestion-1].QuizAnswers)
                        {
                            if (item1.LocalId == 1)
                            {
                                Answer1.Text = item1.Answer;
                            }
                            if (item1.LocalId == 2)
                            {
                                Answer2.Text = item1.Answer;
                            }
                            if (item1.LocalId == 3)
                            {
                                Answer3.Text = item1.Answer;
                            }
                            if (item1.LocalId == 4)
                            {
                                Answer4.Text = item1.Answer;
                            }
                        }
                        TimeDelay = item.QuizQuestions[mydata.SelectedQuestion - 1].Timer;
                        BitmapImage image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = new Uri(item.QuizQuestions[mydata.SelectedQuestion - 1].Picture);
                        image.EndInit();
                        QuestionImage.Source = image;
                        MovePRSB();
                        StartCounter();
                    }
                }
            }
        }


        private async void MovePRSB()
        {
            TimerProgressbar.EndAngle = 360;
            double test = (double)360 / 1000 * 100 / TimeDelay;
            while (TimerProgressbar.EndAngle > 0)
            {
                await Task.Delay(100);
                TimerProgressbar.EndAngle -= test;
                if (TimerProgressbar.EndAngle <= 0)
                {
                    mydata.QuizActive = false;
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
