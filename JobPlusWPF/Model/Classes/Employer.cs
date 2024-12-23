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

        public Employer(string name, string address)
        {
            Name = name;
            Address = address;
        }
    }

}
