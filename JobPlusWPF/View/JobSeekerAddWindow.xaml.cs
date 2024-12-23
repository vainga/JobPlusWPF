using JobPlusWPF.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
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
    /// Логика взаимодействия для JobSeekerAddWindow.xaml
    /// </summary>
    public partial class JobSeekerAddWindow : Window
    {
   
        public JobSeekerAddWindow(JobSeekerAddViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

        }

        private void OnPhotoBorderClick(object sender, RoutedEventArgs e)
        {
            //// Запускаем команду выбора фото при клике на область для фото
            //var viewModel = (JobSeekerAddViewModel)DataContext;
            ////viewModel.LoadPhotoCommand.Execute(null);
        }
    }
}
