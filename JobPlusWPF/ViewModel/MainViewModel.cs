using JobPlusWPF.Model.Interfaces;
using JobPlusWPF.View;
using JobPlusWPF.ViewModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly INavigator _navigationService;
    private string _selectedRole;

    public ObservableCollection<string> ComboBoxItems { get; } = new ObservableCollection<string>
    {
        "Работодатели",
        "Вакансии",
        "Соискатели",
        "Совпадения"
    };

    public string SelectedRole
    {
        get => _selectedRole;
        set
        {
            if (_selectedRole != value)
            {
                _selectedRole = value;
                OnPropertyChanged();
            }
        }
    }

    public ICommand AddCommand { get; }

    public MainViewModel(INavigator navigator)
    {
        _navigationService = navigator;
        AddCommand = new RelayCommand(OnAdd, CanAdd);
    }

    private void OnAdd(object parameter)
    {
        if (SelectedRole == "Соискатели")
        {
            _navigationService.ShowDialog<JobSeekerAddWindow>();
        }
    }

    private bool CanAdd(object parameter) => !string.IsNullOrEmpty(SelectedRole);

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
