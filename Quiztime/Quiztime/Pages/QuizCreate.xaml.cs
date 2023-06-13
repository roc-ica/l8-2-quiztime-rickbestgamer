using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
            Console.WriteLine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName);
        }

        private void ImagePicker(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image files|*.bmp;*.jpg;*.jepg;*.gif;*.png";
            file.FilterIndex = 1;
            if (file.ShowDialog() == true)
            {
                File.Copy(file.FileName, System.IO.Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Media\", System.IO.Path.GetFileName(file.FileName)), true);
                var image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(System.IO.Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Media\", System.IO.Path.GetFileName(file.FileName)));
                image.EndInit();
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
    }
}
