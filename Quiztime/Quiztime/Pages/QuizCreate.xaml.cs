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
            mydata FCall = new mydata();
            FCall.NewQuestion();
            QuizGrid.ItemsSource = mydata.NewQuizQuestion;
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
            mydata FCall = new mydata();
            FCall.NewQuestion();
            QuizGrid.ItemsSource = null;
            QuizGrid.ItemsSource = mydata.NewQuizQuestion;
            await Task.Delay(1);
            foreach (RadioButton tb in FindVisualChildren<RadioButton>(this))
            {
                if (int.Parse((string)tb.Tag) == mydata.NewQuizQuestion[int.Parse(tb.GroupName)-1].CAnswer)
                {
                    tb.IsChecked = true;
                }
            }
            foreach (Button tb in FindVisualChildren<Button>(this))
            {
                if (!string.IsNullOrEmpty(mydata.NewQuizQuestion[int.Parse((string)tb.Tag)-1].Picture))
                {
                    tb.Content = mydata.NewQuizQuestion[int.Parse((string)tb.Tag) - 1].Picture;
                }
            }
        }

        private void AddPicture(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image files|*.bmp;*.jpg;*.jepg;*.gif;*.png";
            file.FilterIndex = 1;
            if (file.ShowDialog() == true)
            {
                File.Copy(file.FileName, System.IO.Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Media\", System.IO.Path.GetFileName(file.FileName)), true);
                mydata.NewQuizQuestion[(int)((Button)sender).Tag - 1].Picture = System.IO.Path.GetFileName(file.FileName);
                ((Button)sender).Content = System.IO.Path.GetFileName(file.FileName);
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
            foreach (var item in mydata.NewQuizQuestion)
            {
            }
        }

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
