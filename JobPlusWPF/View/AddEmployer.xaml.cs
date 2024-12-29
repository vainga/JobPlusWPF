﻿using JobPlusWPF.ViewModel;
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

namespace JobPlusWPF.View
{
    /// <summary>
    /// Логика взаимодействия для AddEmployer.xaml
    /// </summary>
    public partial class AddEmployer : Window
    {
        public AddEmployer(EmplyerAddViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
        //private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    var viewModel = (EmplyerAddViewModel)DataContext;

        //    viewModel.Name = string.Empty;
        //    viewModel.StreetName = string.Empty;
        //    viewModel.CityName = string.Empty;
        //    viewModel.Phone = string.Empty;
        //}
    }
}
