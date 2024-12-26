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
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using JobPlusWPF.Service;

namespace JobPlusWPF.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly INavigator _navigationService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IJobSeekerService _jobSeekerService;
        private readonly IEmplyerService _emplyerService;
        private readonly ICurrentUserService _currentUserService;

        private readonly IRepository<CityDirectory> _cityRepository;
        private readonly IRepository<StreetDirectory> _streetRepository;
        private readonly IRepository<EducationLevel> _educationLevelRepository;

        private readonly IRepository<JobSeeker> _jobSeekerRepository;

        private string _selectedRole;
        private ObservableCollection<JobSeeker> _jobSeekers;
        private UserControl _currentUserControl;

        public ICommand DownloadCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }


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
                    LoadUserControl(); // Загружаем нужный UserControl
                }
            }
        }

        public ObservableCollection<JobSeeker> JobSeekers
        {
            get => _jobSeekers ??= new ObservableCollection<JobSeeker>(); 
            set
            {
                _jobSeekers = value;
                OnPropertyChanged();
            }
        }


        public UserControl CurrentUserControl
        {
            get => _currentUserControl;
            set
            {
                _currentUserControl = value;
                OnPropertyChanged();
            }
        }

        private readonly AppDbContext _dbContext;

        public MainViewModel(INavigator navigator, IServiceProvider serviceProvider, AppDbContext dbContext, IRepository<JobSeeker> jobSeekerRepository, ICurrentUserService currentUserService, IJobSeekerService jobSeekerService, IEmplyerService emplyerService)
        {
            _navigationService = navigator;
            _dbContext = dbContext;
            _serviceProvider = serviceProvider;
            _jobSeekerRepository = jobSeekerRepository;

            _currentUserService = currentUserService;
            _jobSeekerService = jobSeekerService;
            _emplyerService = emplyerService;



            AddCommand = new RelayCommand(OnAdd, CanAdd);
            DownloadCommand = new RelayCommand(OnDownload);
            DeleteCommand = new RelayCommand(OnDelete, CanDelete);
            RefreshCommand = new RelayCommand(OnRefresh);
            EditCommand = new RelayCommand(OnEdit, CanEdit);

        }


        ///Очень плохо, надо изменить потом 
        private async void OnDelete(object parameter)
        {
            if (CurrentUserControl is JobSekeerDataGrid dataGrid && dataGrid.DataContext is JobSeekerDataGridViewModel jobSeekerViewModel)
            {
                var selectedJobSeeker = jobSeekerViewModel.SelectedJobSeeker;

                if (selectedJobSeeker != null)
                {
                    var result = MessageBox.Show($"Вы уверены, что хотите удалить {selectedJobSeeker.Name} {selectedJobSeeker.Surname}?",
                                                 "Подтверждение удаления",
                                                 MessageBoxButton.YesNo,
                                                 MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        await _jobSeekerRepository.DeleteAsync(selectedJobSeeker.Id);
                        JobSeekers.Remove(selectedJobSeeker);
                        jobSeekerViewModel.JobSeekers.Remove(selectedJobSeeker);
                        jobSeekerViewModel.SelectedJobSeeker = null;
                    }
                }
            }
        }

        private bool CanDelete(object parameter)
        {
            if (CurrentUserControl is JobSekeerDataGrid dataGrid && dataGrid.DataContext is JobSeekerDataGridViewModel jobSeekerViewModel)
            {
                return jobSeekerViewModel.SelectedJobSeeker != null;
            }

            return false;
        }

        public ICommand EditCommand { get; }
        private async void OnEdit(object parameter)
        {
            if (CurrentUserControl is JobSekeerDataGrid dataGrid && dataGrid.DataContext is JobSeekerDataGridViewModel jobSeekerViewModel)
            {
                var selectedJobSeeker = jobSeekerViewModel.SelectedJobSeeker;

                //selectedJobSeeker.City = await _cityRepository.FindByIdAsync(selectedJobSeeker.CityId);
                //selectedJobSeeker.Street = await _streetRepository.FindByIdAsync(selectedJobSeeker.StreetId);
                //selectedJobSeeker.EducationLevel = await _educationLevelRepository.FindByIdAsync(selectedJobSeeker.EducationLevelId);

                if (selectedJobSeeker != null)
                {
                    var viewModel = new JobSeekerAddViewModel(
                        _navigationService,
                        _jobSeekerService,
                        _currentUserService,
                        selectedJobSeeker
                    );

                    var editWindow = new JobSeekerAddWindow(viewModel);
                    bool? result = editWindow.ShowDialog();

                    if (result == true)
                    {
                        await _jobSeekerRepository.UpdateAsync(selectedJobSeeker);
                        jobSeekerViewModel.SelectedJobSeeker = selectedJobSeeker;
                    }
                }
            }
        }

        private bool CanEdit(object parameter)
        {
            if (CurrentUserControl is JobSekeerDataGrid dataGrid && dataGrid.DataContext is JobSeekerDataGridViewModel jobSeekerViewModel)
            {
                return jobSeekerViewModel.SelectedJobSeeker != null;
            }

            return false;
        }


        private void LoadUserControl()
        {
            if (SelectedRole == "Соискатели")
            {
                var viewModel = _serviceProvider.GetRequiredService<JobSeekerDataGridViewModel>();
                CurrentUserControl = new JobPlusWPF.View.JobSekeerDataGrid(viewModel);
            }
            else if(SelectedRole == "Работодатели")
            {
                var viewModel = _serviceProvider.GetRequiredService<EmplyerDataGridViewModel>();
                CurrentUserControl = new JobPlusWPF.View.EmplyerDataGrid(viewModel);
            }
            else
            {
                // Очистка или замена на другой UserControl
                CurrentUserControl = null;
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

        private void OnAdd(object parameter)
        {
            if (SelectedRole == "Соискатели")
            {
                _navigationService.ShowDialog<JobSeekerAddWindow>();
                OnRefresh(null);
            }
            if (SelectedRole == "Работодатели")
            {
                _navigationService.ShowDialog<AddEmployer>();
                OnRefresh(null);
            }
            if(SelectedRole == "Вакансии")
            {
                _navigationService.ShowDialog<AddVacancy>();
                OnRefresh(null);
            }

        }



        private async void OnRefresh(object parameter)
        {
            var jobSeekers = await _jobSeekerRepository.GetAllAsync();

            JobSeekers.Clear();

            foreach (var jobSeeker in jobSeekers)
            {
                JobSeekers.Add(jobSeeker);
            }
            LoadUserControl();
        }


        private bool CanAdd(object parameter) => !string.IsNullOrEmpty(SelectedRole);

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}