using Microsoft.Win32;
using Quiztime.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAnimatedGif;
using Xceed.Wpf.Toolkit;

namespace Quiztime.Pages
{
    /// <summary>
    /// Interaction logic for QuizCreate.xaml
    /// </summary>
    public partial class QuizCreate : Window
    {
        public QuizCreate()
        {
            InitializeComponent();
            if (mydata.CoverPicture != null)
            {
                CoverPic.Content = mydata.CoverPicture;
            }

            if (mydata.NewQuizName != null)
            {
                QuizTitleTxb.Text = mydata.NewQuizName;
                QuizTitleTxb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000"));
            }
            else
            {
                QuizTitleTxb.Text = "Enter quiz title here";
            }
            QuizGrid.ItemsSource = mydata.NewQuizQuestion;
            InitialRbSelector();
        }

        private async void InitialRbSelector()
        {
            await Task.Delay(1);
            foreach (RadioButton tb in FindVisualChildren<RadioButton>(this))
            {
                if (int.Parse((string)tb.Tag) == mydata.NewQuizQuestion[int.Parse(tb.GroupName) - 1].CAnswer)
                {
                    tb.IsChecked = true;
                }
            }

        }

        private void PlaceholderRemove(object sender, RoutedEventArgs e)
        {
            if (QuizTitleTxb.Text == "Enter quiz title here")
            {
                QuizTitleTxb.Text = "";
                QuizTitleTxb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000"));
            }
        }

        private void PlaceholderReturn(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(QuizTitleTxb.Text))
            {
                QuizTitleTxb.Text = "Enter quiz title here";
                QuizTitleTxb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF808080"));
            }
        }

        private async void AddQuestion(object sender, RoutedEventArgs e)
        {
            //mydata FCall = new mydata();
            //FCall.NewQuestion();
            mydata.NewQuizQuestion.Add(new NewQuizQuestions());
            mydata.NewQuizQuestion[mydata.NewQuizQuestion.Count - 1].Id = mydata.NewQuizQuestion.Count;
            mydata.NewQuizQuestion[mydata.NewQuizQuestion.Count - 1].Amount = 4;
            mydata.NewQuizQuestion[mydata.NewQuizQuestion.Count - 1].Picture = "";
            QuizGrid.ItemsSource = null;
            QuizGrid.ItemsSource = mydata.NewQuizQuestion;
            await Task.Delay(1);
            foreach (RadioButton tb in FindVisualChildren<RadioButton>(this))
            {
                if (int.Parse((string)tb.Tag) == mydata.NewQuizQuestion[int.Parse(tb.GroupName) - 1].CAnswer)
                {
                    tb.IsChecked = true;
                }
            }
        }

        private void AddPicture(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image files|*.bmp;*.jpg;*.jepg;*.png";
            file.FilterIndex = 1;
            if (file.ShowDialog() == true)
            {
                File.Copy(file.FileName, System.IO.Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Media\", System.IO.Path.GetFileName(file.FileName)), true);
                mydata.NewQuizQuestion[(int)((Button)sender).Tag - 1].Picture = System.IO.Path.GetFileName(file.FileName);
                QuizGrid.ItemsSource = null;
                QuizGrid.ItemsSource = mydata.NewQuizQuestion;
            }
        }


        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield return (T)Enumerable.Empty<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject ithChild = VisualTreeHelper.GetChild(depObj, i);
                if (ithChild == null) continue;
                if (ithChild is T t) yield return t;
                foreach (T childOfChild in FindVisualChildren<T>(ithChild)) yield return childOfChild;
            }
        }

        private void SetCorrectAnswer(object sender, RoutedEventArgs e)
        {
            mydata.NewQuizQuestion[int.Parse(((RadioButton)sender).GroupName) - 1].CAnswer = int.Parse((string)((RadioButton)sender).Tag);
        }

        private void SaveQuiz(object sender, RoutedEventArgs e)
        {
            mydata fc = new mydata();
            if (mydata.QuizMode == 0)
            {
                CorrectError.Child = null;
                NoRowError.Child = null;
                NameError.Child = null;
                QuestionError.Child = null;

                if (fc.QuizNameExcist() && mydata.NewQuizName != "Enter quiz title here")
                {
                    if (mydata.NewQuizQuestion.Count > 0)
                    {
                        int QuestionNotEnterd = 0;
                        int AnswerNotEnterd = 0;
                        foreach (var item in mydata.NewQuizQuestion)
                        {
                            if (item.Question == null)
                            {
                                QuestionNotEnterd++;
                            }
                            if (item.Answer1 == null)
                            {
                                AnswerNotEnterd++;
                            }
                            if (item.Answer2 == null)
                            {
                                AnswerNotEnterd++;
                            }
                            if (item.Answer3 == null)
                            {
                                AnswerNotEnterd++;
                            }
                            if (item.Answer4 == null)
                            {
                                AnswerNotEnterd++;
                            }
                            AnswerNotEnterd = AnswerNotEnterd + mydata.NewQuizQuestion[item.Id - 1].Amount - 4;
                        }
                        if (QuestionNotEnterd != 0)
                        {
                            Label lbl = new Label();
                            lbl.Content = "You have " + QuestionNotEnterd + " questions without the question specified";
                            lbl.Foreground = Brushes.Red;
                            lbl.BorderBrush = Brushes.Red;
                            lbl.BorderThickness = new Thickness(1);
                            lbl.HorizontalAlignment = HorizontalAlignment.Stretch;
                            lbl.VerticalAlignment = VerticalAlignment.Stretch;
                            lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
                            lbl.VerticalContentAlignment = VerticalAlignment.Center;
                            lbl.Margin = new Thickness(5);
                            QuestionError.Child = lbl;
                        }
                        else if (AnswerNotEnterd != 0)
                        {
                            Label lbl = new Label();
                            lbl.Content = "You have " + AnswerNotEnterd + " questions without answers";
                            lbl.Foreground = Brushes.Red;
                            lbl.BorderBrush = Brushes.Red;
                            lbl.BorderThickness = new Thickness(1);
                            lbl.HorizontalAlignment = HorizontalAlignment.Stretch;
                            lbl.VerticalAlignment = VerticalAlignment.Stretch;
                            lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
                            lbl.VerticalContentAlignment = VerticalAlignment.Center;
                            lbl.Margin = new Thickness(5);
                            AnswerError.Child = lbl;
                        }
                        else
                        {
                            int CorrectNotAnswerd = 0;
                            foreach (var item in mydata.NewQuizQuestion)
                            {
                                if (item.CAnswer == 0)
                                {
                                    CorrectNotAnswerd++;
                                }
                            }
                            if (CorrectNotAnswerd != 0)
                            {
                                Label lbl = new Label();
                                lbl.Content = "You have " + CorrectNotAnswerd + " questions without a correct answer";
                                lbl.Foreground = Brushes.Red;
                                lbl.BorderBrush = Brushes.Red;
                                lbl.BorderThickness = new Thickness(1);
                                lbl.HorizontalAlignment = HorizontalAlignment.Stretch;
                                lbl.VerticalAlignment = VerticalAlignment.Stretch;
                                lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
                                lbl.VerticalContentAlignment = VerticalAlignment.Center;
                                lbl.Margin = new Thickness(5);
                                CorrectError.Child = lbl;
                            }
                            else
                            {
                                fc.SaveNewQuiz();
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        Label lbl = new Label();
                        lbl.Content = "You can't save a quiz with no questions";
                        lbl.Foreground = Brushes.Red;
                        lbl.BorderBrush = Brushes.Red;
                        lbl.BorderThickness = new Thickness(1);
                        lbl.HorizontalAlignment = HorizontalAlignment.Stretch;
                        lbl.VerticalAlignment = VerticalAlignment.Stretch;
                        lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
                        lbl.VerticalContentAlignment = VerticalAlignment.Center;
                        lbl.Margin = new Thickness(5);
                        NoRowError.Child = lbl;
                    }
                }
                else
                {
                    if (mydata.NewQuizName == "Enter quiz title here")
                    {
                        Label txbl = new Label();
                        txbl.Content = "Please give the quiz a name";
                        txbl.Foreground = Brushes.Red;
                        txbl.HorizontalAlignment = HorizontalAlignment.Stretch;
                        txbl.VerticalAlignment = VerticalAlignment.Stretch;
                        txbl.HorizontalContentAlignment = HorizontalAlignment.Center;
                        txbl.VerticalContentAlignment = VerticalAlignment.Center;
                        txbl.BorderBrush = Brushes.Red;
                        txbl.BorderThickness = new Thickness(1);
                        txbl.Margin = new Thickness(5);
                        NameError.Child = txbl;
                    }
                    else
                    {
                        Label txbl = new Label();
                        txbl.Content = "This name already excists";
                        txbl.Foreground = Brushes.Red;
                        txbl.HorizontalAlignment = HorizontalAlignment.Stretch;
                        txbl.VerticalAlignment = VerticalAlignment.Stretch;
                        txbl.HorizontalContentAlignment = HorizontalAlignment.Center;
                        txbl.VerticalContentAlignment = VerticalAlignment.Center;
                        txbl.BorderBrush = Brushes.Red;
                        txbl.BorderThickness = new Thickness(1);
                        txbl.Margin = new Thickness(5);
                        NameError.Child = txbl;

                    }
                }
            }
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)sender).Name == "_1")
            {
                mydata.NewQuizQuestion[Convert.ToInt32(((TextBox)sender).Tag) - 1].Answer1 = ((TextBox)sender).Text;
            }
            else if (((TextBox)sender).Name == "_2")
            {
                mydata.NewQuizQuestion[Convert.ToInt32(((TextBox)sender).Tag) - 1].Answer2 = ((TextBox)sender).Text;
            }
            else if (((TextBox)sender).Name == "_3")
            {
                mydata.NewQuizQuestion[Convert.ToInt32(((TextBox)sender).Tag) - 1].Answer3 = ((TextBox)sender).Text;
            }
            else if (((TextBox)sender).Name == "_4")
            {
                mydata.NewQuizQuestion[Convert.ToInt32(((TextBox)sender).Tag) - 1].Answer4 = ((TextBox)sender).Text;
            }
            else if (((TextBox)sender).Name == "Name")
            {
                mydata.NewQuizQuestion[Convert.ToInt32(((TextBox)sender).Tag) - 1].Question = ((TextBox)sender).Text;
            }
        }

        private void RemoveQuestion(object sender, RoutedEventArgs e)
        {
            mydata.NewQuizQuestion.Remove(mydata.NewQuizQuestion[Convert.ToInt32(((Button)sender).Tag) - 1]);
            if (mydata.NewQuizQuestion.Count >= Convert.ToInt32(((Button)sender).Tag))
            {
                for (int i = 0; i < mydata.NewQuizQuestion.Count - (Convert.ToInt32(((Button)sender).Tag) - 1); i++)
                {
                    mydata.NewQuizQuestion[Convert.ToInt32(((Button)sender).Tag) - 1 + i].Id = Convert.ToInt32(((Button)sender).Tag) + i;
                }
            }
            QuizGrid.ItemsSource = null;
            QuizGrid.ItemsSource = mydata.NewQuizQuestion;
        }

        private void QuizTitleTxb_TextChanged(object sender, TextChangedEventArgs e)
        {
            mydata.NewQuizName = ((TextBox)sender).Text;
        }

        private void AddCoverPicture(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image files|*.bmp;*.jpg;*.jepg;*.png";
            file.FilterIndex = 1;
            if (file.ShowDialog() == true)
            {
                File.Copy(file.FileName, System.IO.Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Media\", System.IO.Path.GetFileName(file.FileName)), true);
                mydata.CoverPicture = System.IO.Path.GetFileName(file.FileName);
                ((Button)sender).Content = System.IO.Path.GetFileName(file.FileName);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            QuizGrid.CancelEdit();
        }

        private void IntegerUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            mydata.NewQuizQuestion[Convert.ToInt32(((IntegerUpDown)sender).Tag) - 1].Timer = (int)((IntegerUpDown)sender).Value;
        }

        private async void AmountChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            List<TextBox> tblist = new List<TextBox>();
            List<RadioButton> rblist = new List<RadioButton>();
            await Task.Delay(1);
            foreach (TextBox tb in FindVisualChildren<TextBox>(QuizGrid))
            {
                if (Convert.ToInt32(tb.Tag) == Convert.ToInt32(((IntegerUpDown)sender).Tag))
                {
                    if (tb.Name == "_1")
                    {
                        tblist.Add(tb);
                    }
                    else if (tb.Name == "_2")
                    {
                        tblist.Add(tb);
                    }
                    else if (tb.Name == "_3")
                    {
                        tblist.Add(tb);
                    }
                    else if (tb.Name == "_4")
                    {
                        tblist.Add(tb);
                    }
                }
            }
            foreach (RadioButton rb in FindVisualChildren<RadioButton>(QuizGrid))
            {
                if (Convert.ToInt32(rb.GroupName) == Convert.ToInt32(((IntegerUpDown)sender).Tag))
                {
                    rblist.Add(rb);
                }
            }
            if (tblist.Count == 4 && rblist.Count == 4)
            {
                for (int i = 4; i > ((IntegerUpDown)sender).Value; i--)
                {
                    tblist[i - 1].Visibility = Visibility.Collapsed;
                    tblist[i - 1].Text = null;
                    rblist[i - 1].Visibility = Visibility.Collapsed;
                    rblist[i - 1].IsChecked = false;

                }
                for (int i = 0; i < ((IntegerUpDown)sender).Value; i++)
                {
                    tblist[i].Visibility = Visibility.Visible;
                    rblist[i].Visibility = Visibility.Visible;
                }

            }
            mydata.NewQuizQuestion[Convert.ToInt32(((IntegerUpDown)sender).Tag) - 1].Amount = (int)((IntegerUpDown)sender).Value;
        }

        //private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    mydata.NewQuizQuestion[Convert.ToInt32(((ComboBox)sender).Tag) - 1].Amount = Convert.ToInt32(((ComboBox)sender).SelectedValue);
        //}

        //private void AddPicture(object sender, MouseButtonEventArgs e)
        //{
        //    OpenFileDialog file = new OpenFileDialog();
        //    file.Filter = "Image files|*.bmp;*.jpg;*.jepg;*.gif;*.png";
        //    file.FilterIndex = 1;
        //    if (file.ShowDialog() == true)
        //    {
        //        File.Copy(file.FileName, System.IO.Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Media\", System.IO.Path.GetFileName(file.FileName)), true);
        //        var image = new BitmapImage();
        //        image.BeginInit();
        //        image.UriSource = new Uri(System.IO.Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Media\", System.IO.Path.GetFileName(file.FileName)));
        //        image.EndInit();
        //        ImageBehavior.SetAnimatedSource((Image)sender, image);
        //    }
        //}
    }
}
