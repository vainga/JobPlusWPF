using JobPlusWPF.Model.Classes;
using JobPlusWPF.Model.Interfaces;
using JobPlusWPF.Service;
using JobPlusWPF.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JobPlusWPF.ViewModel
{
    public class EmplyerAddViewModel : INotifyPropertyChanged
    {
        private readonly INavigator _navigator;
        private readonly IEmplyerService _emloyerService;
        private readonly ICurrentUserService _currentUserService;

        public ICommand CancelCommand { get; }
        public ICommand SaveCommand { get; }

        private Employer _editingEmployer;

        private string _name;
        private string _cityName;
        private CityDirectory _city;
        private string _streetName;
        private StreetDirectory _street;
        public string _phone;

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

        private void Cancel(object parameter)
        {
            _navigator.CloseWindow<AddEmployer>();
        }

        private async void Save(object parameter)
        {
            int userId = _currentUserService.GetCurrentUserId();

            if (City == null && !string.IsNullOrEmpty(CityName))
            {
                City = await _emloyerService.GetCityByNameAsync(CityName);
                if (City == null)
                {
                    City = new CityDirectory(CityName);
                    await _emloyerService.AddCityAsync(City);
                }
            }

            if (Street == null && !string.IsNullOrEmpty(StreetName))
            {
                Street = await _emloyerService.GetStreetByNameAsync(StreetName);
                if (Street == null)
                {
                    Street = new StreetDirectory(StreetName);
                    await _emloyerService.AddStreetAsync(Street);
                }
            }

            Employer employer;

            if (_editingEmployer != null)
            {

                 employer = new Employer(
                    Name,
                    City.Id,
                    Street.Id,
                    Phone,
                    userId
                    );

                employer.SetId(_editingEmployer.Id);

                await _emloyerService.UpdareEmployerAsync(employer);
            }
            else
            {
                employer = new Employer(
                    Name,
                    City.Id,
                    Street.Id,
                    Phone,
                    userId
                    );

                await _emloyerService.AddEmplyerAsync(employer);
            }
            _navigator.CloseWindow<AddEmployer>();

        }

        public EmplyerAddViewModel(INavigator navigator, IEmplyerService employerService, ICurrentUserService currentUserService)
        {
            _navigator = navigator;
            _emloyerService = employerService;
            _currentUserService = currentUserService;

            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(Save);

        }

        public EmplyerAddViewModel(INavigator navigator, IEmplyerService employerService, ICurrentUserService currentUserService, Employer employer) : this(navigator, employerService, currentUserService)
        {
            if (employer != null)
            {
                _editingEmployer = employer;

                Name = employer.Name;
                CityName = employer.City.Name;
                StreetName = employer.Name;
                Phone = employer.Phone;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
