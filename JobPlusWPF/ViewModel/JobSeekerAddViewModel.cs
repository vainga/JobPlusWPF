using JobPlusWPF.Model.Classes;
using JobPlusWPF.Model.Interfaces;
using JobPlusWPF.View;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace JobPlusWPF.ViewModel
{
    public class JobSeekerAddViewModel : INotifyPropertyChanged
    {
        private readonly INavigator _navigator;

        private string _name;
        private string _surname;
        private int _age;
        private string _passportNumber;
        private DateTime _passportIssueDate;
        private string _passportIssuedBy;
        private string _phone;
        private string _photo;
        private int _cityId;
        private CityDirectory _city;
        private int _streetId;
        private StreetDirectory _street;
        private int _educationLevelId;
        private EducationLevel _educationLevel;
        private string _institution;
        private string _educationDocumentScan;
        private string _specialty;
        private int _workExperience;
        private DateTime _registrationDate;

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

        public int CityId
        {
            get => _cityId;
            set
            {
                if (_cityId != value)
                {
                    _cityId = value;
                    OnPropertyChanged(nameof(CityId));
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

        public int StreetId
        {
            get => _streetId;
            set
            {
                if (_streetId != value)
                {
                    _streetId = value;
                    OnPropertyChanged(nameof(StreetId));
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

        public int WorkExperience
        {
            get => _workExperience;
            set
            {
                if (_workExperience != value)
                {
                    _workExperience = value;
                    OnPropertyChanged(nameof(WorkExperience));
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


        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CancelCommand { get; }

        private void Cancel(object parameter)
        {
            _navigator.CloseWindow<JobSeekerAddWindow>();
        }

        public JobSeekerAddViewModel(INavigator navigator)
        {
            _navigator = navigator;
            CancelCommand = new RelayCommand(Cancel);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
