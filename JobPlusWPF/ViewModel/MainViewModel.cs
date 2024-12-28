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

    //Заменить репозитории на сервисы
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
        private readonly IRepository<Employer> _employerRepository;
        private readonly IRepository<Vacancy> _vacancyRepository;

        //private readonly IRepository<Employer> _employerRepository;
        //private readonly IRepository<Vacancy> _vacancyRepository;

        private string _selectedRole;
        private ObservableCollection<JobSeeker> _jobSeekers;
        private ObservableCollection<Employer> _employers;
        private ObservableCollection<Vacancy> _vacancies;

        private UserControl _currentUserControl;

        public ICommand DownloadCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public ICommand EditEmployerCommand { get; }
        public ICommand DeleteEmployerCommand { get; }

        public ICommand EditVacancyCommand { get; }
        public ICommand DeleteVacancyCommand { get; }

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
                    LoadUserControl();
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

        public ObservableCollection<Employer> Employers
        {
            get => _employers ??= new ObservableCollection<Employer>();
            set
            {
                _employers = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Vacancy> Vacancies
        {
            get => _vacancies ??= new ObservableCollection<Vacancy>();
            set
            {
                _vacancies = value;
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
            DeleteCommand = new RelayCommand(OnDeleteJobSeeker, CanDeleteJobSeeker);
            RefreshCommand = new RelayCommand(OnRefresh);

            EditCommand = new RelayCommand(OnEditJobSeeker, CanEditJobSeeker);

            EditEmployerCommand = new RelayCommand(OnEditEmployer, CanEditEmployer);
            DeleteEmployerCommand = new RelayCommand(OnDeleteEmployer, CanDeleteEmployer);

            //EditVacancyCommand = new RelayCommand(OnEditVacancy, CanEditVacancy);
            DeleteVacancyCommand = new RelayCommand(OnDeleteVacancy, CanDeleteVacancy);

            //LoadUserControl();

        }

        private async void OnDeleteJobSeeker(object parameter)
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

        private bool CanDeleteJobSeeker(object parameter)
        {
            if (CurrentUserControl is JobSekeerDataGrid dataGrid && dataGrid.DataContext is JobSeekerDataGridViewModel jobSeekerViewModel)
            {
                return jobSeekerViewModel.SelectedJobSeeker != null;
            }

            return false;
        }

        private async void OnDeleteEmployer(object parameter)
        {
            if (CurrentUserControl is EmplyerDataGrid dataGrid && dataGrid.DataContext is EmplyerDataGridViewModel emplyerAddViewModel)
            {
                var selectedEmployer = emplyerAddViewModel.SelectedEmployer;

                if (selectedEmployer != null)
                {
                    var result = MessageBox.Show($"Вы уверены, что хотите удалить {selectedEmployer.Name}?",
                                                 "Подтверждение удаления",
                                                 MessageBoxButton.YesNo,
                                                 MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        await _employerRepository.DeleteAsync(selectedEmployer.Id);
                        Employers.Remove(selectedEmployer);
                        emplyerAddViewModel.Employers.Remove(selectedEmployer);
                        emplyerAddViewModel.SelectedEmployer = null;
                    }
                }
            }
        }

        private bool CanDeleteEmployer(object parameter)
        {
            if (CurrentUserControl is EmplyerDataGrid dataGrid && dataGrid.DataContext is EmplyerDataGridViewModel emplyerAddViewModel)
            {
                return emplyerAddViewModel.SelectedEmployer != null;
            }

            return false;
        }

        private async void OnDeleteVacancy(object parameter)
        {
            if (CurrentUserControl is VacancyDataGrid dataGrid && dataGrid.DataContext is VacancyDataGridViewModel vacancyViewModel)
            {
                var selectedVacancy = vacancyViewModel.SelectedVacancy;

                if (selectedVacancy != null)
                {
                    var result = MessageBox.Show($"Вы уверены, что хотите удалить вакансию {selectedVacancy.Position}?",
                                                 "Подтверждение удаления",
                                                 MessageBoxButton.YesNo,
                                                 MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        await _vacancyRepository.DeleteAsync(selectedVacancy.Id);
                        Vacancies.Remove(selectedVacancy);
                        vacancyViewModel.Vacancies.Remove(selectedVacancy);
                        vacancyViewModel.SelectedVacancy = null;
                    }
                }
            }
        }

        private bool CanDeleteVacancy(object parameter)
        {
            if (CurrentUserControl is VacancyDataGrid dataGrid && dataGrid.DataContext is VacancyDataGridViewModel vacancyViewModel)
            {
                return vacancyViewModel.SelectedVacancy != null;
            }

            return false;
        }


        public ICommand EditCommand { get; }
        private async void OnEditJobSeeker(object parameter)
        {
            if (CurrentUserControl is JobSekeerDataGrid dataGrid && dataGrid.DataContext is JobSeekerDataGridViewModel jobSeekerViewModel)
            {
                var selectedJobSeeker = jobSeekerViewModel.SelectedJobSeeker;

                if (selectedJobSeeker != null)
                {
                    var viewModel = new JobSeekerAddViewModel(
                        _navigationService,
                        _jobSeekerService,
                        _currentUserService,
                        selectedJobSeeker
                    );

                    var editWindow = new JobSeekerAddWindow(viewModel);
                    editWindow.ShowDialog();

                    //await _jobSeekerRepository.DeleteAsync(selectedJobSeeker.Id);
                    OnRefresh(null);
                    //await _jobSeekerRepository.UpdateAsync(selectedJobSeeker);
                    //OnDeleteJobSeeker(jobSeekerViewModel.SelectedJobSeeker);
                }
            }
        }

        private bool CanEditJobSeeker(object parameter)
        {
            if (CurrentUserControl is JobSekeerDataGrid dataGrid && dataGrid.DataContext is JobSeekerDataGridViewModel jobSeekerViewModel)
            {
                return jobSeekerViewModel.SelectedJobSeeker != null;
            }

            return false;
        }

        private async void OnEditEmployer(object parameter)
        {
            if (CurrentUserControl is EmplyerDataGrid dataGrid && dataGrid.DataContext is EmplyerDataGridViewModel employerViewModel)
            {
                var selectedEmployer = employerViewModel.SelectedEmployer;

                if (selectedEmployer != null)
                {
                    var viewModel = new EmplyerAddViewModel(
                        _navigationService,
                        _emplyerService,
                        _currentUserService,
                        selectedEmployer
                    );

                    var editWindow = new AddEmployer(viewModel);
                    bool? result = editWindow.ShowDialog();

                    if (result == true)
                    {
                        await _employerRepository.UpdateAsync(selectedEmployer);
                        employerViewModel.SelectedEmployer = selectedEmployer;
                    }
                }
            }
        }

        private bool CanEditEmployer(object parameter)
        {
            if (CurrentUserControl is EmplyerDataGrid dataGrid && dataGrid.DataContext is EmplyerDataGridViewModel employerViewModel)
            {
                return employerViewModel.SelectedEmployer != null;
            }

            return false;
        }



        private async void LoadUserControl()
        {
            try
            {
                switch (SelectedRole)
                {
                    case "Соискатели":
                        var jobSeekerViewModel = _serviceProvider.GetRequiredService<JobSeekerDataGridViewModel>();
                        await jobSeekerViewModel.LoadJobSeekers();
                        CurrentUserControl = new JobSekeerDataGrid(jobSeekerViewModel);
                        break;

                    case "Работодатели":
                        var employerViewModel = _serviceProvider.GetRequiredService<EmplyerDataGridViewModel>();
                        await employerViewModel.LoadEmployers();
                        CurrentUserControl = new EmplyerDataGrid(employerViewModel);
                        break;

                    case "Вакансии":
                        var vacancyViewModel = _serviceProvider.GetRequiredService<VacancyDataGridViewModel>();
                        await vacancyViewModel.LoadVacanciesAsync();
                        CurrentUserControl = new VacancyDataGrid(vacancyViewModel);
                        break;

                    default:
                        CurrentUserControl = null;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            switch (SelectedRole)
            {
                case "Соискатели":
                    var jobSeekerViewModel = CurrentUserControl?.DataContext as JobSeekerDataGridViewModel;
                    if (jobSeekerViewModel != null)
                    {
                        await jobSeekerViewModel.LoadJobSeekers();
                    }
                    break;

                case "Работодатели":
                    var employerViewModel = CurrentUserControl?.DataContext as EmplyerDataGridViewModel;
                    if (employerViewModel != null)
                    {
                        await employerViewModel.LoadEmployers();
                    }
                    break;

                case "Вакансии":
                    var vacancyViewModel = CurrentUserControl?.DataContext as VacancyDataGridViewModel;
                    if (vacancyViewModel != null)
                    {
                        await vacancyViewModel.LoadVacanciesAsync();
                    }
                    break;
            }
        }



        private bool CanAdd(object parameter) => !string.IsNullOrEmpty(SelectedRole);

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}