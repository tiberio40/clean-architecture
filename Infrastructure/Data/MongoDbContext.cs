using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Infrastructure.Configurations;
using Core.DTOs.TmetricDtos;
using Core.Interfaces;
using Core.Entities.Oauth;


namespace Infrastructure.Data
{

    public class MongoDbContext: IMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Person> Persons => _database.GetCollection<Person>("Persons");
        public IMongoCollection<AuthConfigEntity> AuthConfigurations => _database.GetCollection<AuthConfigEntity>("AuthConfigurations");
        public IMongoCollection<LogDto> Logs => _database.GetCollection<LogDto>("Logs");
        public IMongoCollection<SimplifiedTimeEntryDto> Imputaciones=> _database.GetCollection<SimplifiedTimeEntryDto>("Imputaciones");
        public IMongoCollection<MembersDto> Members => _database.GetCollection<MembersDto>("Members");
        public IMongoCollection<SimplifiedMembersDto> Member => _database.GetCollection<SimplifiedMembersDto>("Members");
    }
}
