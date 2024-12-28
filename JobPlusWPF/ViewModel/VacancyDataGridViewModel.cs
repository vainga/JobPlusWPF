using JobPlusWPF.DBLogic;
using JobPlusWPF.Model.Classes;
using JobPlusWPF.Service;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JobPlusWPF.ViewModel
{
    public class VacancyDataGridViewModel : INotifyPropertyChanged
    {
        private readonly IVacancyService _vacancyService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEmplyerService _emplyerService;

        private ObservableCollection<Vacancy> _vacancies;
        private Vacancy _selectedVacancy;

        public ObservableCollection<Vacancy> Vacancies
        {
            get => _vacancies;
            set
            {
                if (_vacancies != value)
                {
                    _vacancies = value;
                    OnPropertyChanged(nameof(Vacancies));
                }
            }
        }

        public Vacancy SelectedVacancy
        {
            get => _selectedVacancy;
            set
            {
                if (_selectedVacancy != value)
                {
                    _selectedVacancy = value;
                    OnPropertyChanged(nameof(SelectedVacancy));
                }
            }
        }

        //public ICommand LoadVacanciesCommand { get; }

        public VacancyDataGridViewModel(IVacancyService vacancyService, ICurrentUserService currentUserService, IEmplyerService emplyerService)
        {
            _vacancyService = vacancyService;
            _currentUserService = currentUserService;
            //LoadVacanciesCommand = new RelayCommand(async _ => await LoadVacanciesAsync());
            _emplyerService = emplyerService;

            Vacancies = new ObservableCollection<Vacancy>();
            LoadVacanciesAsync();
        }

        //public async Task LoadVacanciesAsync()
        //{
        //    var allVacancies = await _vacancyService.GetAllVacanciesAsync();
        //    var filteredVacancies = allVacancies.Where(v => v.Employer.UserId == _currentUserService.GetCurrentUserId());
        //    Vacancies.Clear();
        //    foreach (var vacancy in filteredVacancies)
        //        try
        //        {
        //            Vacancies.Add(vacancy);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Ошибка при загрузке вакансии {vacancy.Id}: {ex.Message}");
        //        }
        //}

        public async Task LoadVacanciesAsync()
        {
            int currentUserId = _currentUserService.GetCurrentUserId();
            Vacancies.Clear();

            using (var context = new AppDbContext()) // Новый экземпляр контекста
            {
                var vacancies = await context.Vacancies
                    .Where(v => v.Employer.UserId == currentUserId)
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
