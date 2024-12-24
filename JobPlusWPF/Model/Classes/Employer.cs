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
        public string Address { get; private set; }
        public ICollection<Vacancy> Vacancies { get; private set; } = new List<Vacancy>();

        public int UserId { get; private set; }
        public User User { get; private set; }

        public Employer(string name, string address, int userId)
        {
            Name = name;
            Address = address;
            UserId = userId;
        }
    }

}
