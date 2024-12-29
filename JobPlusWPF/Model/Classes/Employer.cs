using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlusWPF.Model.Classes
{
    public class Employer
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int CityId { get; private set; } 
        public CityDirectory City { get; set; }
        public int StreetId { get; private set; }
        public string Phone { get; private set; }
        public StreetDirectory Street { get; set; }
        public ICollection<Vacancy> Vacancies { get; private set; } = new List<Vacancy>();

        public int UserId { get; private set; }
        public User User { get; private set; }

        public Employer(string name, int cityId, int streetId, string phone, int userId)
        {
            Name = name;
            CityId = cityId;
            StreetId = streetId;
            UserId = userId;
            Phone = phone;
        }

        public void SetId(int id) => Id = id;

        public void Update(
        string name,
        int cityId,
        int streetId,
        string phone)
        {
            Name = name;
            CityId = cityId;
            StreetId = streetId;
            Phone = phone;
        }

    }


}
