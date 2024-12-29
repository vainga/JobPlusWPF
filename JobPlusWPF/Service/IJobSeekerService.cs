using JobPlusWPF.Model.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlusWPF.Service
{
    public interface IJobSeekerService
    {
        Task AddJobSeekerAsync(JobSeeker jobSeeker);
        Task<IEnumerable<EducationLevel>> GetEducationLevelsAsync();
        Task<CityDirectory> GetCityByNameAsync(string cityName);
        Task AddCityAsync(CityDirectory city);
        Task<StreetDirectory> GetStreetByNameAsync(string streetName);
        Task AddStreetAsync(StreetDirectory street);
        Task UpdateJobSeekerAsync(JobSeeker jobSeeker);
        Task<IEnumerable<Status>> GetStatusesAsync();
    }
}
