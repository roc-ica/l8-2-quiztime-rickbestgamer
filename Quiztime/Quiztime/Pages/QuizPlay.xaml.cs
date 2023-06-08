﻿using Quiztime.Classes;
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
        }
    }
}