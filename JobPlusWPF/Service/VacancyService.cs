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

        public async Task UpdateVacancyAsync(Vacancy vacancy)
        {
            if (vacancy == null) throw new ArgumentNullException(nameof(vacancy));

            var existingVacancy = await _vacancyRepository.FindByIdAsync(vacancy.Id);
            if (existingVacancy == null)
                throw new InvalidOperationException($"JobSeeker with ID {vacancy.Id} does not exist.");

            // Обновляем поля
            existingVacancy.Update(
                vacancy.Position,
                vacancy.EmployerId,
                vacancy.Salary,
                vacancy.Description
            );

            await _vacancyRepository.UpdateAsync(existingVacancy);
        }
    }
}
