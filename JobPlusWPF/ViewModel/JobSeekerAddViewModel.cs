using JobPlusWPF.DBLogic;
using JobPlusWPF.Model.Classes;
using JobPlusWPF.Model.Interfaces;
using JobPlusWPF.Service;
using JobPlusWPF.View;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;

namespace JobPlusWPF.ViewModel
{
    public class JobSeekerAddViewModel : INotifyPropertyChanged
    {
        public event Action JobSeekerAdded;

        private readonly INavigator _navigator;
        private readonly IJobSeekerService _jobSeekerService;
        private readonly ICurrentUserService _currentUserService;

        private JobSeeker _editingJobSeeker;

        private string _name;
        private string _surname;
        private int _age;
        private string _passportNumber;
        private DateTime _passportIssueDate = DateTime.UtcNow;
        private string _passportIssuedBy;
        private string _phone;
        private string _photo;
        private string _cityName;
        private CityDirectory _city;
        private string _streetName;
        private StreetDirectory _street;
        private int _educationLevelId;
        //private EducationLevel _educationLevel;
        private string _institution;
        private string _educationDocumentScan;
        private string _specialty;
        private int _workExperience;
        private DateTime _registrationDate;
        private int _workExperienceYears;
        private int _workExperienceMonths;
        private int _statusId;
        private EducationLevel _selectedEducationLevel;
        private IEnumerable<EducationLevel> _educationLevels;
        
        private Status _selectedStatus;
        private IEnumerable<Status> _statuses = new ObservableCollection<Status>();

        private string _educationDocumentFileName;

        //Для архива
        private readonly IArchiveService _archiveService;
        private readonly IVacancyService _vacancyService;
        private ObservableCollection<Vacancy> _vacancies;
        private Vacancy _selectedVacancy;
        private int _vacancyId;
        private bool _isVacancyComboBoxVisible;

        // Коллекция вакансий
        public ObservableCollection<Vacancy> Vacancies
        {
            get { return _vacancies; }
            set
            {
                _vacancies = value;
                OnPropertyChanged(nameof(Vacancies));
            }
        }

        // Выбранная вакансия
        public Vacancy SelectedVacancy
        {
            get { return _selectedVacancy; }
            set
            {
                _selectedVacancy = value;
                OnPropertyChanged(nameof(SelectedVacancy));
                VacancyId = _selectedVacancy?.Id ?? 0;
            }
        }

        // ID выбранной вакансии
        public int VacancyId
        {
            get { return _vacancyId; }
            set
            {
                _vacancyId = value;
                OnPropertyChanged(nameof(VacancyId));
            }
        }

        public bool IsVacancyComboBoxVisible => SelectedStatus?.Id == 2;
        public bool IsAllowanceTextBoxVisible => SelectedStatus?.Id == 3;



        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                if (_surname != value)
                {
                    _surname = value;
                    OnPropertyChanged(nameof(Surname));
                }
            }
        }

        public int Age
        {
            get => _age;
            set
            {
                if (_age != value)
                {
                    _age = value;
                    OnPropertyChanged(nameof(Age));
                }
            }
        }

        public string PassportNumber
        {
            get => _passportNumber;
            set
            {
                if (_passportNumber != value)
                {
                    _passportNumber = value;
                    OnPropertyChanged(nameof(PassportNumber));
                }
            }
        }

        public DateTime PassportIssueDate
        {
            get => _passportIssueDate;
            set
            {
                if (_passportIssueDate != value)
                {
                    _passportIssueDate = value;
                    OnPropertyChanged(nameof(PassportIssueDate));
                }
            }
        }

        public string PassportIssuedBy
        {
            get => _passportIssuedBy;
            set
            {
                if (_passportIssuedBy != value)
                {
                    _passportIssuedBy = value;
                    OnPropertyChanged(nameof(PassportIssuedBy));
                }
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    OnPropertyChanged(nameof(Phone));
                }
            }
        }

        public string Photo
        {
            get => _photo;
            set
            {
                if (_photo != value)
                {
                    _photo = value;
                    OnPropertyChanged(nameof(Photo));
                }
            }
        }

        public string CityName
        {
            get => _cityName;
            set
            {
                if (_cityName != value)
                {
                    _cityName = value;
                    OnPropertyChanged(nameof(CityName));
                }
            }
        }

        public CityDirectory City
        {
            get => _city;
            set
            {
                if (_city != value)
                {
                    _city = value;
                    OnPropertyChanged(nameof(City));
                }
            }
        }

        public string StreetName
        {
            get => _streetName;
            set
            {
                if (_streetName != value)
                {
                    _streetName = value;
                    OnPropertyChanged(nameof(StreetName));
                }
            }
        }

        public StreetDirectory Street
        {
            get => _street;
            set
            {
                if (_street != value)
                {
                    _street = value;
                    OnPropertyChanged(nameof(Street));
                }
            }
        }

        public int EducationLevelId
        {
            get => _educationLevelId;
            set
            {
                if (_educationLevelId != value)
                {
                    _educationLevelId = value;
                    OnPropertyChanged(nameof(EducationLevelId));
                }
            }
        }

        public int StatusId
        {
            get => _statusId;
            set
            {
                if (_statusId != value)
                {
                    _statusId = value;
                    OnPropertyChanged(nameof(StatusId));
                }
            }
        }

        //public EducationLevel EducationLevel
        //{
        //    get => _educationLevel;
        //    set
        //    {
        //        if (_educationLevel != value)
        //        {
        //            _educationLevel = value;
        //            OnPropertyChanged(nameof(EducationLevel));
        //        }
        //    }
        //}

        public string Institution
        {
            get => _institution;
            set
            {
                if (_institution != value)
                {
                    _institution = value;
                    OnPropertyChanged(nameof(Institution));
                }
            }
        }

        public string EducationDocumentScan
        {
            get => _educationDocumentScan;
            set
            {
                if (_educationDocumentScan != value)
                {
                    _educationDocumentScan = value;
                    OnPropertyChanged(nameof(EducationDocumentScan));
                }
            }
        }

        public string Specialty
        {
            get => _specialty;
            set
            {
                if (_specialty != value)
                {
                    _specialty = value;
                    OnPropertyChanged(nameof(Specialty));
                }
            }
        }

        public int WorkExperienceYears
        {
            get => _workExperienceYears;
            set
            {
                if (_workExperienceYears != value)
                {
                    _workExperienceYears = value;
                    OnPropertyChanged(nameof(WorkExperienceYears));
                }
            }
        }

        public int WorkExperienceMonths
        {
            get => _workExperienceMonths;
            set
            {
                if (_workExperienceMonths != value)
                {
                    _workExperienceMonths = value;
                    OnPropertyChanged(nameof(WorkExperienceMonths));
                }
            }
        }

        public IEnumerable<EducationLevel> EducationLevels
        {
            get => _educationLevels;
            set
            {
                if (_educationLevels != value)
                {
                    _educationLevels = value;
                    OnPropertyChanged(nameof(EducationLevels));
                }
            }
        }

        public EducationLevel SelectedEducationLevel
        {
            get => _selectedEducationLevel;
            set
            {
                if (_selectedEducationLevel != value)
                {
                    _selectedEducationLevel = value;
                    OnPropertyChanged(nameof(SelectedEducationLevel));
                }
            }
        }

        public IEnumerable<Status> Statuses
        {
            get
            {
                if (_editingJobSeeker == null)
                {
                    // Возвращаем коллекцию с единственным статусом
                    return new List<Status> { _statuses.FirstOrDefault(s => s.Id == 1) };
                }

                return _statuses;
            }
            set
            {
                if (_statuses != value)
                {
                    _statuses = value;
                    OnPropertyChanged(nameof(Statuses));
                }
            }
        }


        public Status SelectedStatus
        {
            get => _selectedStatus;
            set
            {
                if (_selectedStatus != value)
                {
                    _selectedStatus = value;
                    OnPropertyChanged(nameof(SelectedStatus));
                    OnPropertyChanged(nameof(IsVacancyComboBoxVisible));
                    OnPropertyChanged(nameof(IsAllowanceTextBoxVisible));
                }
            }
        }


        public DateTime RegistrationDate
        {
            get => _registrationDate;
            set
            {
                if (_registrationDate != value)
                {
                    _registrationDate = value;
                    OnPropertyChanged(nameof(RegistrationDate));
                }
            }
        }

        public string EducationDocumentFileName
        {
            get { return _educationDocumentFileName; }
            set
            {
                _educationDocumentFileName = value;
                OnPropertyChanged(nameof(EducationDocumentFileName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CancelCommand { get; }
        public ICommand SelectPhotoCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand LoadEducationDocumentCommand { get; }


        private void Cancel(object parameter)
        {
            _navigator.CloseWindow<JobSeekerAddWindow>();
        }



        private void SelectPhoto(object parameter)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Выберите фотографию"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                Photo = openFileDialog.FileName;
            }
        }

        private void LoadEducationDocument(object parameter)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "PDF Files|*.pdf"
            };

            if (dialog.ShowDialog() == true)
            {
                string filePath = dialog.FileName;
                string fileExtension = Path.GetExtension(filePath);
                string fileName = Path.GetFileName(filePath);

                EducationDocumentFileName = fileName;

                string destinationFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JobSeekerPDFs");
                Directory.CreateDirectory(destinationFolder);

                string encryptedFileName = Guid.NewGuid().ToString() + fileExtension;
                string destinationPath = Path.Combine(destinationFolder, encryptedFileName);

                try
                {
                    File.Copy(filePath, destinationPath);
                    EducationDocumentScan = destinationPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading document: " + ex.Message);
                }
            }
        }


        private async void Save(object parameter)
        {
            try
            {
                // Сохранение фото
                if (!string.IsNullOrEmpty(Photo))
                {
                    Photo = SavePhoto(Photo);
                }

                await SaveJobSeeker();
                ClearFields();

                MessageBox.Show("Сохранение прошло успешно");
                JobSeekerAdded?.Invoke();
                _navigator.CloseWindow<JobSeekerAddWindow>();

            }
            catch (Exception ex)
            {
                string errorMessage = "Сохранение прошло неудачно: " + ex.Message;

                // Проверяем и выводим внутреннее исключение, если оно есть
                if (ex.InnerException != null)
                {
                    errorMessage += "\nВнутреннее исключение: " + ex.InnerException.Message;
                }

                MessageBox.Show(errorMessage);
            }
        }


        private string SavePhoto(string photoPath)
        {
            string projectFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JobSeekerImages");
            Directory.CreateDirectory(projectFolder);

            string fileName = Path.GetFileName(photoPath);
            string destinationPath = Path.Combine(projectFolder, fileName);

            if (File.Exists(destinationPath))
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(photoPath);
                destinationPath = Path.Combine(projectFolder, fileName);
            }

            File.Copy(photoPath, destinationPath, true);

            return destinationPath;
        }

        private async Task SaveJobSeeker()
        {
            int userId = _currentUserService.GetCurrentUserId();

            if (City == null && !string.IsNullOrEmpty(CityName))
            {
                City = await _jobSeekerService.GetCityByNameAsync(CityName);
                if (City == null)
                {
                    City = new CityDirectory(CityName);
                    await _jobSeekerService.AddCityAsync(City);
                }
            }

            if (Street == null && !string.IsNullOrEmpty(StreetName))
            {
                Street = await _jobSeekerService.GetStreetByNameAsync(StreetName);
                if (Street == null)
                {
                    Street = new StreetDirectory(StreetName);
                    await _jobSeekerService.AddStreetAsync(Street);
                }
            }

            PassportIssueDate = PassportIssueDate.ToUniversalTime();

            JobSeeker jobSeeker;

            if (_editingJobSeeker != null)
            {
                // Создание обновлённого экземпляра для редактируемого пользователя
                jobSeeker = new JobSeeker(
                    Name,
                    Surname,
                    Age,
                    PassportNumber,
                    PassportIssueDate,
                    PassportIssuedBy,
                    Phone,
                    Photo,
                    City.Id,
                    Street.Id,
                    EducationLevelId,
                    Institution,
                    EducationDocumentScan,
                    Specialty,
                    WorkExperienceYears * 12 + WorkExperienceMonths,
                    _editingJobSeeker.RegistrationDate,
                    userId,
                    StatusId
                );

                jobSeeker.SetId(_editingJobSeeker.Id);           

                await _jobSeekerService.UpdateJobSeekerAsync(jobSeeker);

                if (SelectedStatus.Id == 2)
                {
                    var archiveEntry = new ArchiveEntry(SelectedVacancy.Id, jobSeeker.Id, DateTime.UtcNow, userId);
                    await _vacancyService.ArchiveVacancyAsync(SelectedVacancy);
                    await _archiveService.AddArchiveEntryAsync(archiveEntry);
                }

            }
            else
            {
                jobSeeker = new JobSeeker(
                    Name,
                    Surname,
                    Age,
                    PassportNumber,
                    PassportIssueDate,
                    PassportIssuedBy,
                    Phone,
                    Photo,
                    City.Id,
                    Street.Id,
                    EducationLevelId,
                    Institution,
                    EducationDocumentScan,
                    Specialty,
                    WorkExperienceYears * 12 + WorkExperienceMonths,
                    DateTime.UtcNow,
                    userId,
                    StatusId
                );

                await _jobSeekerService.AddJobSeekerAsync(jobSeeker);
            }
        }



        private void ClearFields()
        {
            Name = string.Empty;
            Surname = string.Empty;
            Age = 0;
            PassportNumber = string.Empty;
            PassportIssueDate = DateTime.UtcNow;
            PassportIssuedBy = string.Empty;
            Phone = string.Empty;
            Photo = string.Empty;
            City = null;
            CityName = string.Empty;
            Street = null;
            StreetName = string.Empty;
            EducationLevelId = 0;
            SelectedEducationLevel = null;
            Institution = string.Empty;
            EducationDocumentScan = string.Empty;
            Specialty = string.Empty;
            WorkExperienceYears = 0;
            WorkExperienceMonths = 0;
            RegistrationDate = DateTime.UtcNow;
            EducationDocumentFileName = string.Empty;
            StatusId = 1;
        }

        private async Task LoadEducationLevels()
        {
            EducationLevels = await _jobSeekerService.GetEducationLevelsAsync();
        }

        private async Task LoadStatuses()
        {
            Statuses = await _jobSeekerService.GetStatusesAsync();  
        }


        public async Task InitializeAsync()
        {
            await LoadStatuses();
            await LoadEducationLevels();
        }


        public JobSeekerAddViewModel(INavigator navigator, IJobSeekerService jobSeekerService, ICurrentUserService currentUserService)
        {
            _navigator = navigator;
            _jobSeekerService = jobSeekerService;
            _currentUserService = currentUserService;
            CancelCommand = new RelayCommand(Cancel);
            SelectPhotoCommand = new RelayCommand(SelectPhoto);
            SaveCommand = new RelayCommand(Save);
            LoadEducationDocumentCommand = new RelayCommand(LoadEducationDocument);
            
            InitializeAsync();
        }

        public JobSeekerAddViewModel(INavigator navigator, IJobSeekerService jobSeekerService, IVacancyService vacancyService, IArchiveService archiveService, ICurrentUserService currentUserService, JobSeeker jobSeeker)
            : this(navigator, jobSeekerService, currentUserService)
        {
            _editingJobSeeker = jobSeeker;
            if (jobSeeker != null)
            {
                Name = jobSeeker.Name;
                Surname = jobSeeker.Surname;
                Age = jobSeeker.Age;
                PassportNumber = jobSeeker.PassportNumber;
                PassportIssueDate = jobSeeker.PassportIssueDate;
                PassportIssuedBy = jobSeeker.PassportIssuedBy;
                Phone = jobSeeker.Phone;
                Photo = jobSeeker.Photo;
                CityName = jobSeeker.City.Name;
                StreetName = jobSeeker.Street.Name;
                EducationLevelId = jobSeeker.EducationLevelId;
                SelectedEducationLevel = jobSeeker.EducationLevel;
                Institution = jobSeeker.Institution;
                EducationDocumentScan = jobSeeker.EducationDocumentScan;
                Specialty = jobSeeker.Specialty;
                StatusId = jobSeeker.StatusId;

                int totalMonths = jobSeeker.WorkExperience;
                WorkExperienceYears = totalMonths / 12;
                WorkExperienceMonths = totalMonths % 12;

                RegistrationDate = jobSeeker.RegistrationDate;
            }
            Vacancies = new ObservableCollection<Vacancy>();
            LoadVacanciesAsync();
            _archiveService = archiveService;
            _vacancyService = vacancyService;
        }

        public async Task LoadVacanciesAsync()
        {
            int currentUserId = _currentUserService.GetCurrentUserId();
            Vacancies.Clear();

            using (var context = new AppDbContext())
            {
                var vacancies = await context.Vacancies
                    .Where(v => v.Employer.UserId == currentUserId && !v.IsArchived)
                    .Include(v => v.Employer)
                    .ToListAsync();
                foreach (var vacancy in vacancies)
                {
                    if (!Vacancies.Any(v => v.Id == vacancy.Id))
                    {
                        Vacancies.Add(vacancy);
                    }
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
