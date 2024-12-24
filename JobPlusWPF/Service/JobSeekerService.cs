using JobPlusWPF.DBLogic;
using JobPlusWPF.Model.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPlusWPF.Service
{
    public class JobSeekerService : IJobSeekerService
    {
        private readonly IRepository<JobSeeker> _jobSeekerRepository;
        private readonly IRepository<CityDirectory> _cityRepository;
        private readonly IRepository<StreetDirectory> _streetRepository;
        private readonly IRepository<EducationLevel> _educationLevelRepository;


        public JobSeekerService(
            IRepository<JobSeeker> jobSeekerRepository,
            IRepository<CityDirectory> cityRepository,
            IRepository<StreetDirectory> streetRepository,
            IRepository<EducationLevel> educationLevelRepository)
        {
            _jobSeekerRepository = jobSeekerRepository ?? throw new ArgumentNullException(nameof(jobSeekerRepository));
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
            _streetRepository = streetRepository ?? throw new ArgumentNullException(nameof(streetRepository));
            _educationLevelRepository = educationLevelRepository ?? throw new ArgumentNullException(nameof(educationLevelRepository));
        }

        public async Task AddJobSeekerAsync(JobSeeker jobSeeker)
        {
            if (jobSeeker == null) throw new ArgumentNullException(nameof(jobSeeker));

            await _jobSeekerRepository.AddAsync(jobSeeker);
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
    }
}
