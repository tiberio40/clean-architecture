using Core.DTOs.TmetricDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMembersRequestService
    {
        Task<IEnumerable<MembersDto>> GetAllMembers(string account);
        Task<MembersDto> GetMemberById(string id);
    }

}
