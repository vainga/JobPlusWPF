using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlusWPF.Model.Classes
{
    public class Vacancy
    {
        public int Id { get; private set; }
        public string Position { get; private set; }
        public decimal Salary { get; private set; }
        public string Description { get; private set; }
        public int EmployerId { get; private set; }
        public Employer Employer { get; private set; }

        public bool IsArchived { get; private set; }

        public Vacancy(string position, int employerId, decimal salary, string description)
        {
            Position = position;
            EmployerId = employerId;
            Salary = salary;
            Description = description;
        }

        public void SetId(int id) => Id = id;

        public void Update(string position, int employerId, decimal salary, string description)
        {
            Position = position;
            EmployerId = employerId;
            Salary = salary;
            Description = description;
            IsArchived = false;
        }

        public void Archive(bool isarchive = true)
        {
            IsArchived = isarchive;
        }
    }

}
