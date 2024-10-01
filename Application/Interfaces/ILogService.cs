using Core.DTOs.TmetricDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ILogService
    {
        Task AddLog(LogDto log);
        Task<IEnumerable<LogDto>> GetAllLogs();
    }
}
