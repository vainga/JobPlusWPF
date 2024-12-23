using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlusWPF.Model.Classes
{
    public class CityDirectory
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public ICollection<JobSeeker> JobSeekers { get; private set; } = new List<JobSeeker>();

        public CityDirectory(string name)
        {
            Name = name;
        }
    }

}
