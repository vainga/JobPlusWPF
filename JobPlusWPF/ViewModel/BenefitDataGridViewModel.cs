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
    public class BenefitDataGridViewModel : INotifyPropertyChanged
    {
        private readonly IBenefitService _benefitService;
        private readonly ICurrentUserService _currentUserService;

        private ObservableCollection<Benefit> _benefits;
        private Benefit _selectedBenefit;

        public ObservableCollection<Benefit> Benefits
        {
            get => _benefits;
            set
            {
                if (_benefits != value)
                {
                    _benefits = value;
                    OnPropertyChanged(nameof(Benefits));
                }
            }
        }

        public Benefit SelectedBenefit
        {
            get => _selectedBenefit;
            set
            {
                if (_selectedBenefit != value)
                {
                    _selectedBenefit = value;
                    OnPropertyChanged(nameof(SelectedBenefit));
                }
            }
        }

        public ICommand LoadBenefitsCommand { get; }

        public BenefitDataGridViewModel(IBenefitService benefitService, ICurrentUserService currentUserService)
        {
            _benefitService = benefitService;
            _currentUserService = currentUserService;

            Benefits = new ObservableCollection<Benefit>();
            LoadBenefitsCommand = new RelayCommand(async _ => await LoadBenefitsAsync());
            LoadBenefitsAsync();
        }

        public async Task LoadBenefitsAsync()
        {
            int currentUserId = _currentUserService.GetCurrentUserId();
            Benefits.Clear();

            using (var context = new AppDbContext()) // Новый экземпляр контекста
            {
                var benefits = await context.Benefits
                    .Where(b => b.UserId == currentUserId) // Загрузка бенефитов для текущего пользователя
                    .Include(b => b.JobSeeker) // Подгрузка соискателей
                    .Include(b => b.User) // Подгрузка пользователей (если нужно)
                    .ToListAsync();

                foreach (var benefit in benefits)
                {
                    if (!Benefits.Any(b => b.Id == benefit.Id))
                    {
                        Benefits.Add(benefit);
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
