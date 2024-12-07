using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace JobPlusWPF.View
{
    public partial class EnterWindow : Window, INotifyPropertyChanged
    {
        private bool _isRegistering = false;
        public event PropertyChangedEventHandler PropertyChanged;

        public EnterWindow()
        {
            InitializeComponent();
            DataContext = this;
            ButtonText = "Вход";
            LableText = "Регистрация";
        }


        private string _buttonText;
        public string ButtonText
        {
            get => _buttonText;
            set
            {
                if (_buttonText != value)
                {
                    _buttonText = value;
                    OnPropertyChanged(nameof(ButtonText)); // Уведомление об изменении свойства
                }
            }
        }

        private string _lableText;
        public string LableText
        {
            get => _lableText;
            set {
                if (_lableText != value)
                {
                    _lableText = value;
                    OnPropertyChanged(nameof(LableText));
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            if (_isRegistering)
            {
                // Логика для регистрации
                //MessageBox.Show("Регистрация...");
            }
            else
            {
                // Логика для входа
                //MessageBox.Show("Вход...");
            }
        }

        private void OnRegisterLabelClick(object sender, RoutedEventArgs e)
        {
            _isRegistering = !_isRegistering;

            ButtonText = _isRegistering ? "Регистрация" : "Вход";
            LableText = _isRegistering ? "Вход" : "Регистрация";

            FirstName.Visibility = _isRegistering ? Visibility.Visible : Visibility.Collapsed;
            LastName.Visibility = _isRegistering ? Visibility.Visible : Visibility.Collapsed;

            //RegisterTextBlock.Text = _isRegistering ? "Вход" : "Регистрация";
            //RegisterTextBlock.TextDecorations = _isRegistering ? TextDecorations.Underline : null;
            //RegisterTextBlock.Foreground = _isRegistering ? Brushes.Blue : Brushes.Blue;
        }
    }
}
