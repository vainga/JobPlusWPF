using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using JobPlusWPF.Model.Interfaces;
using JobPlusWPF.Service;

namespace JobPlusWPF.ViewModel
{
    public class EnterViewModel : INotifyPropertyChanged
    {
        private readonly UserService _userService;
        private readonly INavigator _navigationService;


        private string _login;
        private string _password;
        private string _errorMessage;
        private string _buttonText = "Вход";
        private string _labelText = "Регистрация";

        public event PropertyChangedEventHandler PropertyChanged;

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public string ButtonText
        {
            get => _buttonText;
            set
            {
                _buttonText = value;
                OnPropertyChanged();
            }
        }

        public string LabelText
        {
            get => _labelText;
            set
            {
                _labelText = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand SwitchModeCommand { get; }


        public EnterViewModel(UserService userService, INavigator navigationService)
        {
            _userService = userService;
            _navigationService = navigationService;
            LoginCommand = new RelayCommand(async _ => await ExecuteLoginAsync());
            SwitchModeCommand = new RelayCommand(_ => SwitchMode());
        }


        private async Task ExecuteLoginAsync()
        {
            try
            {
                if (ButtonText == "Вход")
                {
                    var user = await _userService.LoginAsync(Login, Password);

                    if (user != null)
                    {
                        _navigationService.NavigateTo<MainWindow>();
                    }
                    else
                    {
                        ErrorMessage = "Неверные учетные данные";
                    }
                }
                else if (ButtonText == "Регистрация")
                {
                    await _userService.RegisterUserAsync(Login, Password);
                    ErrorMessage = "Регистрация прошла успешно. Теперь выполните вход.";
                    SwitchMode();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }


        private bool CanExecuteLogin()
        {
            return !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);
        }

        private void SwitchMode()
        {
            if (ButtonText == "Вход")
            {
                ButtonText = "Регистрация";
                LabelText = "Вход";
            }
            else
            {
                ButtonText = "Вход";
                LabelText = "Регистрация";
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
