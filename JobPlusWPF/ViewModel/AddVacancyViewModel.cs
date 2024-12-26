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

        public ICommand CancelCommand { get; }
        public ICommand SaveCommand { get; }

        private string _position;
        private string _salary;
        private Employer _selectedEmployer;
        private ObservableCollection<Employer> _employers;

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

        public string Salary
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
            if (SelectedEmployer == null || string.IsNullOrWhiteSpace(Position) || string.IsNullOrWhiteSpace(Salary))
            {
                // Валидация: убедитесь, что все обязательные поля заполнены
                return;
            }

            if (!int.TryParse(Salary, out var parsedSalary))
            {
                // Если зарплата введена некорректно, можно уведомить пользователя или просто выйти
                return;
            }

            var newVacancy = new Vacancy(Position, SelectedEmployer.Id, parsedSalary);

            await _vacancyService.AddVacancyAsync(newVacancy);
            _navigator.CloseWindow<AddVacancy>();
        }


        public AddVacancyViewModel(INavigator navigator, IVacancyService vacancyService ,ICurrentUserService currentUserService)
        {
            _navigator = navigator;
            _currentUserService = currentUserService;
            _vacancyService = vacancyService;

            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(Save);

        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
