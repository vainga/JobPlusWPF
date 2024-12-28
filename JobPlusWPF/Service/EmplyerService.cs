using JobPlusWPF.DBLogic;
using JobPlusWPF.Model.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlusWPF.Service
{
    public class EmplyerService : IEmplyerService
    {
        private readonly IRepository<Employer> _emplyerRepository;
        private readonly IRepository<CityDirectory> _cityRepository;
        private readonly IRepository<StreetDirectory> _streetRepository;
        private readonly IRepository<EducationLevel> _educationLevelRepository;
        private readonly IRepository<Vacancy> _vacancyRepository;

        public EmplyerService(
            IRepository<Employer> emplyerRepository,
            IRepository<CityDirectory> cityRepository,
            IRepository<StreetDirectory> streetRepository,
            IRepository<EducationLevel> educationLevelRepository,
            IRepository<Vacancy> vacancyRepository)
        {
            _emplyerRepository = emplyerRepository ?? throw new ArgumentNullException(nameof(emplyerRepository));
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
            _streetRepository = streetRepository ?? throw new ArgumentNullException(nameof(streetRepository));
            _educationLevelRepository = educationLevelRepository ?? throw new ArgumentNullException(nameof(educationLevelRepository));
            _vacancyRepository = vacancyRepository;
        }

        public async Task AddEmplyerAsync(Employer employer)
        {
            if (employer == null) throw new ArgumentNullException(nameof(employer));

            await _emplyerRepository.AddAsync(employer);
        }

        public async Task<IEnumerable<EducationLevel>> GetEducationLevelsAsync()
        {
            return await _educationLevelRepository.GetAllAsync();
        }


        public async Task<CityDirectory> GetCityByNameAsync(string cityName)
        {
            return await _cityRepository.FindByNameAsync(cityName);
        }

        public async Task AddCityAsync(CityDirectory city)
        {
            if (city == null) throw new ArgumentNullException(nameof(city));

            await _cityRepository.AddAsync(city);
        }

        public async Task<StreetDirectory> GetStreetByNameAsync(string streetName)
        {
            return await _streetRepository.FindByNameAsync(streetName);
        }

        public async Task AddStreetAsync(StreetDirectory street)
        {
            if (street == null) throw new ArgumentNullException(nameof(street));

            await _streetRepository.AddAsync(street);
        }

        public async Task<IEnumerable<Employer>> GetAllEmployersAsync()
        {
            return await _emplyerRepository.GetAllAsync();
        }

        public async Task<Employer> GetEmployerByIdAsync(int id)
        {
            return await _emplyerRepository.FindByIdAsync(id);
        }

        public async Task DeleteEmployerAsync(Employer employer)
        {
            if (employer == null) throw new ArgumentNullException(nameof(employer));

            var vacancies = await _vacancyRepository.FindAsync(vacancy => vacancy.EmployerId == employer.Id);

            foreach (var vacancy in vacancies)
            {
                await _vacancyRepository.DeleteAsync(vacancy.Id);
            }

            await _emplyerRepository.DeleteAsync(employer.Id);
        }


    }
}
