using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlusWPF.Model.Classes
{
    public class Status
    {
        public int Id { get; private set; }
        public string WorkStatus { get; private set; }
        public ICollection<JobSeeker> JobSeekers { get; private set; } = new List<JobSeeker>();

        public Status(string workStatus)
        {
            WorkStatus = workStatus;
        }

    }
}
