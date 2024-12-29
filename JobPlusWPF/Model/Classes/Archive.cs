using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlusWPF.Model.Classes
{
    public class ArchiveEntry
    {
        public int Id { get; private set; }
        public int VacancyId { get; private set; }
        public Vacancy Vacancy { get; private set; }
        public int JobSeekerId { get; private set; }
        public JobSeeker JobSeeker { get; private set; }
        public DateTime TransferDate { get; private set; }
        public ArchiveEntry(int vacancyId, int jobSeekerId, DateTime transferDate)
        {
            VacancyId = vacancyId;
            JobSeekerId = jobSeekerId;
            TransferDate = transferDate;
        }

    }
}
