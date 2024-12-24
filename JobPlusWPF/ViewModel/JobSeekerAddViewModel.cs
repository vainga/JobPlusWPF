using JobPlusWPF.Model.Classes;
using JobPlusWPF.Model.Interfaces;
using JobPlusWPF.Service;
using JobPlusWPF.View;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace JobPlusWPF.ViewModel
{
    public class JobSeekerAddViewModel : INotifyPropertyChanged
    {
        private readonly INavigator _navigator;
        private readonly IJobSeekerService _jobSeekerService;
        private readonly ICurrentUserService _currentUserService;

        private string _name;
        private string _surname;
        private int _age;
        private string _passportNumber;
        private DateTime _passportIssueDate;
        private string _passportIssuedBy;
        private string _phone;
        private string _photo;
        private string _cityName;
        private CityDirectory _city;
        private string _streetName;
        private StreetDirectory _street;
        private int _educationLevelId;
        private EducationLevel _educationLevel;
        private string _institution;
        private string _educationDocumentScan;
        private string _specialty;
        private int _workExperience;
        private DateTime _registrationDate;
        private int _workExperienceYears;
        private int _workExperienceMonths;
        private IEnumerable<EducationLevel> _educationLevels;

        private string _educationDocumentFileName;

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

        public EducationLevel EducationLevel
        {
            get => _educationLevel;
            set
            {
                if (_educationLevel != value)
                {
                    _educationLevel = value;
                    OnPropertyChanged(nameof(EducationLevel));
                }
            }
        }

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
            // Получаем userId с помощью ICurrentUserService
            int userId = _currentUserService.GetCurrentUserId();

            // Проверяем и добавляем город
            if (City == null || string.IsNullOrEmpty(City.Name))
            {
                if (!string.IsNullOrEmpty(CityName))
                {
                    City = await _jobSeekerService.GetCityByNameAsync(CityName);

                    if (City == null) // Город не найден, добавляем в БД
                    {
                        City = new CityDirectory(CityName); // Передаем CityName в конструктор
                        await _jobSeekerService.AddCityAsync(City);
                    }
                }
            }

            // Проверяем и добавляем улицу
            if (Street == null || string.IsNullOrEmpty(Street.Name))
            {
                if (!string.IsNullOrEmpty(StreetName))
                {
                    Street = await _jobSeekerService.GetStreetByNameAsync(StreetName);

                    if (Street == null) // Улица не найдена, добавляем в БД
                    {
                        Street = new StreetDirectory(StreetName); // Передаем StreetName в конструктор
                        await _jobSeekerService.AddStreetAsync(Street);
                    }
                }
            }

            // Создаем новый JobSeeker и сохраняем в базе данных
            JobSeeker newJobSeeker = new JobSeeker(
                Name,
                Surname,
                Age,
                PassportNumber,
                PassportIssueDate,
                PassportIssuedBy,
                Phone,
                Photo,
                City.Id,  // Используем Id города
                Street.Id, // Используем Id улицы
                EducationLevelId,
                Institution,
                EducationDocumentScan,
                Specialty,
                WorkExperienceYears * 12 + WorkExperienceMonths, // Переводим опыт работы в месяцы
                RegistrationDate,
                userId  // Передаем userId, полученный из ICurrentUserService
            );

            await _jobSeekerService.AddJobSeekerAsync(newJobSeeker);
        }

        private void ClearFields()
        {
            Name = string.Empty;
            Surname = string.Empty;
            Age = 0;
            PassportNumber = string.Empty;
            PassportIssueDate = DateTime.MinValue;
            PassportIssuedBy = string.Empty;
            Phone = string.Empty;
            Photo = string.Empty;
            CityName = string.Empty;
            StreetName = string.Empty;
            EducationLevelId = 0;
            Institution = string.Empty;
            EducationDocumentScan = string.Empty;
            Specialty = string.Empty;
            WorkExperienceYears = 0;
            WorkExperienceMonths = 0;
            RegistrationDate = DateTime.MinValue;
            EducationDocumentFileName = string.Empty;
        }

        private async Task LoadEducationLevels()
        {
            EducationLevels = await _jobSeekerService.GetEducationLevelsAsync();
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

            LoadEducationLevels();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
