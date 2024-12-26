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
                OnPropertyChanged(nameof(Employers));
            }
        }

        private Employer _selectedEmployer;
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

        private async Task LoadEmployers()
        {
            int currentUserId = _currentUserService.GetCurrentUserId();
            var employers = await _employerRepository.GetAllAsync();

            var filteredEmployers = employers.Where(e => e.UserId == currentUserId);

            Employers.Clear();
            foreach (var employer in filteredEmployers)
            {
                try
                {
                    var city = await _cityRepository.FindByIdAsync(employer.CityId);
                    var street = await _streetRepository.FindByIdAsync(employer.StreetId);

                    employer.City = city;
                    employer.Street = street;

                    Employers.Add(employer);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при загрузке работодателя {employer.Id}: {ex.Message}");
                }
            }
        }


        public EmplyerDataGridViewModel(IRepository<Employer> employerRepository, ICurrentUserService currentUserService, IRepository<CityDirectory> cityRepository, IRepository<StreetDirectory> streetRepository)
        {
            _employerRepository = employerRepository;
            _currentUserService = currentUserService;
            _cityRepository = cityRepository;
            _streetRepository = streetRepository;

            Employers = new ObservableCollection<Employer>();
            LoadEmployers();
        }

    }
}
