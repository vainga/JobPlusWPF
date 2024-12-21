using System.Windows;
using JobPlusWPF.DBLogic;
using JobPlusWPF.Model.Classes;
using JobPlusWPF.Service;
using JobPlusWPF.ViewModel;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace JobPlusWPF.View
{
    public partial class EnterWindow : Window
    {
        public EnterWindow(EnterViewModel enterViewModel)
        {
            InitializeComponent();
            var viewModel = (EnterViewModel)App.Services.GetRequiredService<EnterViewModel>();
            DataContext = viewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            var viewModel = (EnterViewModel)this.DataContext;
            viewModel.Password = passwordBox.Password;
        }

    }
}
