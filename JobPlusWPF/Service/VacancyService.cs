using JobPlusWPF.DBLogic;
using JobPlusWPF.Model.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlusWPF.Service
{
    public class VacancyService : IVacancyService
    {
        private readonly IRepository<Vacancy> _vacancyRepository;

        public VacancyService(IRepository<Vacancy> vacancyRepository)
        {
            _vacancyRepository = vacancyRepository ?? throw new ArgumentNullException(nameof(vacancyRepository));
        }

        public async Task AddVacancyAsync(Vacancy vacancy)
        {
            if (vacancy == null) throw new ArgumentNullException(nameof(vacancy));

            await _vacancyRepository.AddAsync(vacancy);
        }

        public async Task<IEnumerable<Vacancy>> GetAllVacanciesAsync()
        {
            return await _vacancyRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Vacancy>> GetVacanciesByEmployerIdAsync(int employerId)
        {
            return await _vacancyRepository.FindAsync(vacancy => vacancy.EmployerId == employerId);
        }
    }
}
