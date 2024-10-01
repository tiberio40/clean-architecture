using Core.DTOs.TmetricDtos;
using Core.Interfaces;
using Infrastructure.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TimeEntryRepository : ITimeEntryRepository
    {
        private readonly IMongoCollection<SimplifiedTimeEntryDto> _timeEntryCollection;

        public TimeEntryRepository(MongoDbContext context)
        {
            _timeEntryCollection = context.Imputaciones; 
        }

        public async Task AddTimeEntry(SimplifiedTimeEntryDto Imputaciones)
        {
            await _timeEntryCollection.InsertOneAsync(Imputaciones);
        }

        public async Task<IEnumerable<SimplifiedTimeEntryDto>> GetAllTimeEntries()
        {
            return await _timeEntryCollection.Find(_ => true).ToListAsync();
        }
    }

    
}