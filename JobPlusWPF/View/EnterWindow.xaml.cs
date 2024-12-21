using System.Windows;
using JobPlusWPF.DBLogic;
using JobPlusWPF.Model.Classes;
using JobPlusWPF.Service;
using JobPlusWPF.ViewModel;
using System.Windows.Controls;

namespace JobPlusWPF.View
{
    public partial class EnterWindow : Window
    {
        public EnterWindow(EnterViewModel enterViewModel)
        {
            InitializeComponent();

            var DBContext = new JobPlusWPF.DBLogic.AppDbContext();

            var userRepository = new JobPlusWPF.DBLogic.Repository<User>(DBContext);

            var userService = new UserService(userRepository);

            DataContext = new EnterViewModel(userService);
        }

        //private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        //{
        //    var passwordBox = (PasswordBox)sender;
        //    var viewModel = (EnterViewModel)this.DataContext;
        //    viewModel.Password = passwordBox.Password;
        //}

    }
}
