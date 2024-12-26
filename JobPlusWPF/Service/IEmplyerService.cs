using JobPlusWPF.Model.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlusWPF.Service
{
    public interface IEmplyerService
    {
        Task AddEmplyerAsync(Employer employer);
        Task<IEnumerable<EducationLevel>> GetEducationLevelsAsync();
        Task<CityDirectory> GetCityByNameAsync(string cityName);
        Task AddCityAsync(CityDirectory city);
        Task<StreetDirectory> GetStreetByNameAsync(string streetName);
        Task AddStreetAsync(StreetDirectory street);
        Task<IEnumerable<Employer>> GetAllEmployersAsync();
    }
}
