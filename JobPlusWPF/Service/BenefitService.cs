using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobPlusWPF.DBLogic;
using JobPlusWPF.Model.Classes;

namespace JobPlusWPF.Service
{
    public class BenefitService : IBenefitService
    {
        private readonly IRepository<Benefit> _benefitRepository;

        public BenefitService(IRepository<Benefit> benefitRepository)
        {
            _benefitRepository = benefitRepository ?? throw new ArgumentNullException(nameof(benefitRepository));
        }

        public async Task AddBenefitAsync(Benefit benefit)
        {
            if (benefit == null) throw new ArgumentNullException(nameof(benefit));
            _benefitRepository.AddAsync(benefit);
        }
    }
}
