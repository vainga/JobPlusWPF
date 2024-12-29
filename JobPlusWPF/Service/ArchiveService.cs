using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobPlusWPF.DBLogic;
using JobPlusWPF.Model.Classes;

namespace JobPlusWPF.Service
{
    public class ArchiveService : IArchiveService
    {
        private readonly IRepository<ArchiveEntry> _archiveRepository;

        public ArchiveService(IRepository<ArchiveEntry> archiveRepository)
        {
            _archiveRepository = archiveRepository ?? throw new ArgumentNullException(nameof(archiveRepository));
        }

        public async Task AddArchiveEntryAsync(ArchiveEntry archiveEntry)
        {
            if (archiveEntry == null) throw new ArgumentNullException(nameof(archiveEntry));
            await _archiveRepository.AddAsync(archiveEntry);
        }
    }
}
