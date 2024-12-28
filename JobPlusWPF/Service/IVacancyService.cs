using JobPlusWPF.Model.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlusWPF.Service
{
    public interface IVacancyService
    {
        Task AddVacancyAsync(Vacancy vacancy);
        Task<IEnumerable<Vacancy>> GetAllVacanciesAsync();
        Task<IEnumerable<Vacancy>> GetVacanciesByEmployerIdAsync(int employerId);
    }
}
