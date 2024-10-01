using Application.Interfaces;
using Core.DTOs.TmetricDtos;
using Core.Interfaces;

namespace Application.Services
{
    public class LogService : ILogService
    {

        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task AddLog(LogDto log)
        {
            await _logRepository.AddLog(log);
        }

        public async Task<IEnumerable<LogDto>> GetAllLogs()
        {
            return await _logRepository.GetAllLogs();
        }
    }
}
