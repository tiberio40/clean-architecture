using Core.DTOs.TmetricDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ITimeEntryRepository
    {
        Task AddTimeEntry(SimplifiedTimeEntryDto Imputaciones);
        Task<IEnumerable<SimplifiedTimeEntryDto>> GetAllTimeEntries();
    }
}
