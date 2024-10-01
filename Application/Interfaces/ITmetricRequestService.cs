using Core.DTOs.TmetricDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Application.Interfaces
{
    public interface ITmetricRequestService
    {
        Task<UserProfileDto?> GetUserAdmin();
        Task<List<UserProfileTimeDto>?> GetAllTimeEntry(string account, string startTime, string endTime, bool useUtcTime);
        Task<List<TimeEntryDto>?> GetTimeEntriesByUser(int account, string email, string startTime, string endTime, bool useUtcTime, bool includeDeleted, bool truncate);

    }

}
