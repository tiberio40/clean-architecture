using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IMongoCollection<Person> _personsCollection;

        public PersonRepository(MongoDbContext context)
        {
            _personsCollection = context.Persons;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _personsCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Person> GetByIdAsync(string id)
        {
            return await _personsCollection.Find(person => person.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Person person)
        {
            await _personsCollection.InsertOneAsync(person);
        }

        public async Task UpdateAsync(string id, Person person)
        {
            await _personsCollection.ReplaceOneAsync(p => p.Id == id, person);
        }

        public async Task DeleteAsync(string id)
        {
            await _personsCollection.DeleteOneAsync(person => person.Id == id);
        }
    }
}
