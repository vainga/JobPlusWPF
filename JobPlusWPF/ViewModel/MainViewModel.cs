using JobPlusWPF.DBLogic;
using JobPlusWPF.Model.Classes;
using JobPlusWPF.Model.Interfaces;
using JobPlusWPF.View;
using JobPlusWPF.ViewModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.IO;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly INavigator _navigationService;
    private string _selectedRole;
    private ObservableCollection<JobSeeker> _jobSeekers;

    public ICommand DownloadCommand { get; }

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
                LoadJobSeekers(); // Load JobSeekers when SelectedRole changes
            }
        }
    }

    public ObservableCollection<JobSeeker> JobSeekers
    {
        get => _jobSeekers;
        set
        {
            _jobSeekers = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddCommand { get; }

    private readonly AppDbContext _dbContext;

    public MainViewModel(INavigator navigator, AppDbContext dbContext)
    {
        _navigationService = navigator;
        _dbContext = dbContext;
        AddCommand = new RelayCommand(OnAdd, CanAdd);
        DownloadCommand = new RelayCommand(OnDownload);
    }

    private void LoadJobSeekers()
    {
        if (SelectedRole == "Соискатели")
        {
            var jobSeekersList = _dbContext.JobSeekers.ToList();
            JobSeekers = new ObservableCollection<JobSeeker>(jobSeekersList);
        }
        else
        {
            JobSeekers = new ObservableCollection<JobSeeker>();
        }
    }

    private void OnDownload(object parameter)
    {
        if (parameter is string filePath && !string.IsNullOrWhiteSpace(filePath))
        {
            try
            {
                // Путь для сохранения файла
                string destinationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), Path.GetFileName(filePath));

                // Если файл доступен, копируем его
                if (File.Exists(filePath))
                {
                    File.Copy(filePath, destinationPath, overwrite: true);
                    MessageBox.Show($"Файл успешно сохранён на рабочем столе: {destinationPath}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Файл не найден. Проверьте путь.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
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
