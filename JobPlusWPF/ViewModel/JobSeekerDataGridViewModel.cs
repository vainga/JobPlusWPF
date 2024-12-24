using JobPlusWPF.DBLogic;
using JobPlusWPF.Model.Classes;
using JobPlusWPF.Service;
using JobPlusWPF.ViewModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

public class JobSeekerDataGridViewModel : INotifyPropertyChanged
{
    private readonly IRepository<JobSeeker> _jobSeekerRepository;
    private readonly ICurrentUserService _currentUserService;

    private ObservableCollection<JobSeeker> _jobSeekers;
    public ObservableCollection<JobSeeker> JobSeekers
    {
        get { return _jobSeekers; }
        set
        {
            _jobSeekers = value;
            OnPropertyChanged(nameof(JobSeekers));
        }
    }

    public ICommand DownloadCommand { get; }

    public JobSeekerDataGridViewModel(IRepository<JobSeeker> jobSeekerRepository, ICurrentUserService currentUserService)
    {
        _jobSeekerRepository = jobSeekerRepository;
        _currentUserService = currentUserService;
        JobSeekers = new ObservableCollection<JobSeeker>();
        LoadJobSeekers();

        DownloadCommand = new RelayCommand(OnDownload, CanDownload);
    }

    private async void LoadJobSeekers()
    {
        int currentUserId = _currentUserService.GetCurrentUserId();  // Получаем ID текущего пользователя
        var jobSeekers = await _jobSeekerRepository.GetAllAsync();

        var filteredJobSeekers = jobSeekers.Where(js => js.UserId == currentUserId);

        JobSeekers.Clear();
        foreach (var jobSeeker in filteredJobSeekers)
        {
            JobSeekers.Add(jobSeeker);
        }
    }


    private JobSeeker _selectedJobSeeker;
    public JobSeeker SelectedJobSeeker
    {
        get => _selectedJobSeeker;
        set
        {
            if (_selectedJobSeeker != value)
            {
                _selectedJobSeeker = value;
                OnPropertyChanged(nameof(SelectedJobSeeker));
            }
        }
    }


    private void OnDownload(object parameter)
    {
        if (parameter is string filePath && !string.IsNullOrWhiteSpace(filePath))
        {
            try
            {
                string destinationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), Path.GetFileName(filePath));
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

    private bool CanDownload(object parameter) => parameter is string filePath && !string.IsNullOrWhiteSpace(filePath);

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
