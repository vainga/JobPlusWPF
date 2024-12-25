using JobPlusWPF.DBLogic;
using JobPlusWPF.Model.Classes;
using JobPlusWPF.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlusWPF.ViewModel
{
    public class EmplyerDataGridViewModel : INotifyPropertyChanged
    {
        private readonly IRepository<Employer> _employerRepository;
        private readonly IRepository<CityDirectory> _cityRepository;
        private readonly IRepository<StreetDirectory> _streetRepository;

        private readonly ICurrentUserService _currentUserService;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Employer> _employer;
        public ObservableCollection<Employer> Employers
        {
            get { return _employer; }
            set
            {
                _employer = value;
                OnPropertyChanged(nameof(Employers ));
            }
        }

        private JobSeeker _selectedEmplyers;
        public JobSeeker SelectedEmplyers
        {
            get => _selectedEmplyers;
            set
            {
                if (_selectedEmplyers != value)
                {
                    _selectedEmplyers = value;
                    OnPropertyChanged(nameof(SelectedEmplyers));
                }
            }
        }

        private async void LoadEmployers()
        {
            int currentUserId = _currentUserService.GetCurrentUserId();
            var employers = await _employerRepository.GetAllAsync();

            var filteredEmployers = employers.Where(js => js.UserId == currentUserId);

            //Employers = new ObservableCollection<Employer>();
            Employers.Clear();
            foreach (var employer in filteredEmployers)
            {
                try
                {
                    var city = _cityRepository.FindByIdAsync(employer.CityId);
                    var street = _streetRepository.FindByIdAsync(employer.StreetId);

                    employer.City = await city;
                    employer.Street = await street;

                    Employers.Add(employer);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при загрузке пользователя {employer.Id}: {ex.Message}");
                }
            }
        }

        public EmplyerDataGridViewModel(IRepository<Employer> employerRepository, ICurrentUserService currentUserService, IRepository<CityDirectory> cityRepository, IRepository<StreetDirectory> streetRepository)
        {
            _employerRepository = employerRepository;
            _currentUserService = currentUserService;
            _cityRepository = cityRepository;
            _streetRepository = streetRepository;

            LoadEmployers();
        }

    }
}
