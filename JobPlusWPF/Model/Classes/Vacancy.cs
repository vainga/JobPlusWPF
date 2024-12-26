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
        public int Salary { get; private set; }
        public int EmployerId { get; private set; }
        public Employer Employer { get; private set; }

        public Vacancy(string position, int employerId, int salary)
        {
            Position = position;
            EmployerId = employerId;
            Salary = salary;
        }
    }

}
