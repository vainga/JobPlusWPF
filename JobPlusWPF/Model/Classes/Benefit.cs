using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlusWPF.Model.Classes
{
    public class Benefit
    {
        public int Id { get; private set; }
        public decimal Amount { get; private set; }
        public int JobSeekerId { get; private set; }
        public JobSeeker JobSeeker { get; private set; }
        public DateTime AssignedDate { get; private set; }

        public int UserId { get; private set; }
        public User User { get; private set; }

        public Benefit(decimal amount, int jobSeekerId, DateTime assignedDate, int userId)
        {
            Amount = amount;
            JobSeekerId = jobSeekerId;
            AssignedDate = assignedDate;
            UserId = userId;
        }
    }

}
