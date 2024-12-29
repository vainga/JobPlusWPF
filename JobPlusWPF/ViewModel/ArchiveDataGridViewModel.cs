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
    public class ArchiveDataGridViewModel : INotifyPropertyChanged
    {
        private readonly IArchiveService _archiveEntryService;
        private readonly ICurrentUserService _currentUserService;

        private ObservableCollection<ArchiveEntry> _archiveEntries;
        private ArchiveEntry _selectedArchiveEntry;

        public ObservableCollection<ArchiveEntry> ArchiveEntries
        {
            get => _archiveEntries;
            set
            {
                if (_archiveEntries != value)
                {
                    _archiveEntries = value;
                    OnPropertyChanged(nameof(ArchiveEntries));
                }
            }
        }

        public ArchiveEntry SelectedArchiveEntry
        {
            get => _selectedArchiveEntry;
            set
            {
                if (_selectedArchiveEntry != value)
                {
                    _selectedArchiveEntry = value;
                    OnPropertyChanged(nameof(SelectedArchiveEntry));
                }
            }
        }

        public ICommand LoadArchiveEntriesCommand { get; }

        public ArchiveDataGridViewModel(IArchiveService archiveEntryService, ICurrentUserService currentUserService)
        {
            _archiveEntryService = archiveEntryService;
            _currentUserService = currentUserService;

            ArchiveEntries = new ObservableCollection<ArchiveEntry>();
            LoadArchiveEntriesCommand = new RelayCommand(async _ => await LoadArchiveEntriesAsync());
            LoadArchiveEntriesAsync();
        }

        public async Task LoadArchiveEntriesAsync()
        {
            int currentUserId = _currentUserService.GetCurrentUserId();
            ArchiveEntries.Clear();

            using (var context = new AppDbContext()) // Новый экземпляр контекста
            {
                var archiveEntries = await context.Archive
                    .Where(a => a.Vacancy.Employer.UserId == currentUserId) // Загрузка архива для текущего пользователя
                    .Include(a => a.Vacancy) // Подгрузка вакансий
                    .ThenInclude(v => v.Employer) // Подгрузка работодателей
                    .Include(a => a.JobSeeker) // Подгрузка соискателей
                    .Include(a => a.User) // Подгрузка пользователей (если нужно)
                    .ToListAsync();

                foreach (var entry in archiveEntries)
                {
                    if (!ArchiveEntries.Any(a => a.Id == entry.Id))
                    {
                        ArchiveEntries.Add(entry);
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
