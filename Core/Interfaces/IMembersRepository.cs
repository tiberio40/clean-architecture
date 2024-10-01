using Core.DTOs.TmetricDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMembersRepository
    {
        Task AddMembers(SimplifiedMembersDto Members); 
        Task<IEnumerable<SimplifiedMembersDto>> GetAllMembers();
        Task<int?> GetUserProfileIdByEmail(string email);

        Task<string?> GetEmailByUserProfile(int userProfileId);

    }
}
