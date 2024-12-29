using JobPlusWPF.Model.Classes;
using JobPlusWPF.Model.Interfaces;
using JobPlusWPF.Service;
using JobPlusWPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JobPlusWPF.ViewModel
{
    public class AddVacancyViewModel : INotifyPropertyChanged
    {
        private readonly INavigator _navigator;
        private readonly ICurrentUserService _currentUserService;
        private readonly IVacancyService _vacancyService;
        private readonly IEmplyerService _employerService;

        public ICommand CancelCommand { get; }
        public ICommand SaveCommand { get; }

        private string _position;
        private decimal _salary;
        private Employer _selectedEmployer;
        private string _description;
        private ObservableCollection<Employer> _employers;

        private Vacancy _existingVacancy;

        public string Position
        {
            get => _position;
            set
            {
                if (_position != value)
                {
                    _position = value;
                    OnPropertyChanged(nameof(Position));
                }
            }
        }

        public decimal Salary
        {
            get => _salary;
            set
            {
                if (_salary != value)
                {
                    _salary = value;
                    OnPropertyChanged(nameof(Salary));
                }
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        public Employer SelectedEmployer
        {
            get => _selectedEmployer;
            set
            {
                if (_selectedEmployer != value)
                {
                    _selectedEmployer = value;
                    OnPropertyChanged(nameof(SelectedEmployer));
                }
            }
        }

        public ObservableCollection<Employer> Employers
        {
            get => _employers;
            set
            {
                if (_employers != value)
                {
                    _employers = value;
                    OnPropertyChanged(nameof(Employers));
                }
            }
        }

        private void Cancel(object parameter)
        {
            _navigator.CloseWindow<AddVacancy>();
        }

        private async void Save(object parameter)
        {
            Vacancy vacancy;
            if (SelectedEmployer == null || string.IsNullOrWhiteSpace(Position) /*|| string.IsNullOrWhiteSpace(Salary)*/)
            {
                return;
            }
            if (_existingVacancy != null)
            {
                vacancy = new Vacancy(Position, SelectedEmployer.Id, Salary, Description);
                vacancy.SetId(_existingVacancy.Id);
                await _vacancyService.UpdateVacancyAsync(vacancy);
            }
            else
            {
                vacancy = new Vacancy(Position, SelectedEmployer.Id, Salary, Description);
                await _vacancyService.AddVacancyAsync(vacancy);
            }

            _navigator.CloseWindow<AddVacancy>();
        }

        private async Task LoadEmployersAsync()
        {
            var employers = await _employerService.GetAllEmployersAsync();
            var filteredEmployers = employers.Where(e => e.UserId == _currentUserService.GetCurrentUserId());
            Employers = new ObservableCollection<Employer>(filteredEmployers);
        }

        public AddVacancyViewModel(INavigator navigator, IVacancyService vacancyService ,ICurrentUserService currentUserService, IEmplyerService emplyerService)
        {
            _navigator = navigator;
            _currentUserService = currentUserService;
            _vacancyService = vacancyService;
            _employerService = emplyerService;

            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(Save);

            LoadEmployersAsync();
        }

        public AddVacancyViewModel(INavigator navigator, IVacancyService vacancyService, ICurrentUserService currentUserService, IEmplyerService emplyerService, Vacancy vacancy) : this(navigator, vacancyService, currentUserService, emplyerService)
        {
            if (vacancy != null)
            {
                _existingVacancy = vacancy;

                Position = vacancy.Position;
                SelectedEmployer = vacancy.Employer;
                Salary = vacancy.Salary;
                Description = vacancy.Description;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
