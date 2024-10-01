using Core.DTOs.TmetricDtos;
using Core.Interfaces;
using Infrastructure.Data;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly IMongoCollection<LogDto> _logCollection;

        public LogRepository(MongoDbContext context)
        {
            _logCollection = context.Logs;
        }

        public async Task AddLog(LogDto log)
        {
            await _logCollection.InsertOneAsync(log);
        }

        public async Task<IEnumerable<LogDto>> GetAllLogs()
        {
            return await _logCollection.Find(_ => true).ToListAsync();
        }
    }
}
