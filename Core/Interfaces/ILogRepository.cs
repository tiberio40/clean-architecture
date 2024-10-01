using Core.DTOs.TmetricDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ILogRepository
    {
        Task AddLog(LogDto log);
        Task<IEnumerable<LogDto>> GetAllLogs();
    }
}
