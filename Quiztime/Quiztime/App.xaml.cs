using Quiztime.Pages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Quiztime
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void CloseProgram(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void OpenSettings(object sender, RoutedEventArgs e)
        {
            Window settings = new settings();
            settings.Show();
        }
    }
}
