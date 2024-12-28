using JobPlusWPF.DBLogic;
using JobPlusWPF.Model.Classes;
using JobPlusWPF.Service;
using JobPlusWPF.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

public class JobSeekerDataGridViewModel : INotifyPropertyChanged
{
    private readonly IRepository<JobSeeker> _jobSeekerRepository;
    private readonly IRepository<CityDirectory> _cityRepository;
    private readonly IRepository<StreetDirectory> _streetRepository;
    private readonly IRepository<EducationLevel> _educationLevelRepository;

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

    public JobSeekerDataGridViewModel(IRepository<JobSeeker> jobSeekerRepository, ICurrentUserService currentUserService, IRepository<CityDirectory> cityRepository, IRepository<StreetDirectory> streetRepository, IRepository<EducationLevel> educationLevelRepository)
    {
        _jobSeekerRepository = jobSeekerRepository;
        _currentUserService = currentUserService;
        JobSeekers = new ObservableCollection<JobSeeker>();
        _cityRepository = cityRepository;
        _streetRepository = streetRepository;
        _educationLevelRepository = educationLevelRepository;
        LoadJobSeekers();

        DownloadCommand = new RelayCommand(OnDownload, CanDownload);
    }

    public async Task LoadJobSeekers()
    {
        int currentUserId = _currentUserService.GetCurrentUserId();
        JobSeekers.Clear();

        using (var context = new AppDbContext()) // Новый экземпляр контекста
        {
            var jobSeekers = await context.JobSeekers
                .Where(js => js.UserId == currentUserId)
                .Include(js => js.City)
                .Include(js => js.Street)
                .Include(js => js.EducationLevel)
                .ToListAsync();

            foreach (var jobSeeker in jobSeekers)
            {
                if (!JobSeekers.Any(js => js.Id == jobSeeker.Id))
                {
                    JobSeekers.Add(jobSeeker);
                }
            }
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
