using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlusWPF.Model.Classes
{
    public class EducationLevel
    {
        public int Id { get; private set; }
        public string Level { get; private set; }
        public ICollection<JobSeeker> JobSeekers { get; private set; } = new List<JobSeeker>();

        public EducationLevel(string level)
        {
            Level = level;
        }
    }

}
