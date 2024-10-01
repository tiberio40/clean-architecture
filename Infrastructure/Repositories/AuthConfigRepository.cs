using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;
using MongoDB.Driver;
using Infrastructure.Repositories.Interfaces;
using MongoDB.Bson;
using Core.Entities.Oauth;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class AuthConfigRepository : IAuthConfigRepository
    {
        private readonly IMongoCollection<AuthConfigEntity> _AuthConfigurations;

        public AuthConfigRepository(MongoDbContext context)
        {
            _AuthConfigurations = context.AuthConfigurations;
        }

        public async Task<List<AuthConfigEntity>> GetAllAsync()
        {
            return await _AuthConfigurations.Find(AuthConfig => true).ToListAsync();
        }

        public async Task<AuthConfigEntity> GetByIdAsync(ObjectId id)
        {
            return await _AuthConfigurations.Find<AuthConfigEntity>(AuthConfig => AuthConfig.AuthConfigId == id).FirstOrDefaultAsync();
        }

        public async Task<List<AuthConfigEntity>> FindAll(Expression<Func<AuthConfigEntity, bool>> where)
        {
            return await _AuthConfigurations.Find<AuthConfigEntity>(where).ToListAsync();
        }

        public async Task<AuthConfigEntity> GetByConnectionNameAsync(string connectionName)
        {
            var filter = Builders<AuthConfigEntity>.Filter.Eq("ConnectionName", connectionName);
            return await _AuthConfigurations.Find(filter).FirstOrDefaultAsync();
        }
        public async Task CreateAsync(AuthConfigEntity AuthConfig)
        {
            await _AuthConfigurations.InsertOneAsync(AuthConfig);
        }

        public async Task UpdateAsync(ObjectId id, AuthConfigEntity AuthConfig)
        {
            await _AuthConfigurations.ReplaceOneAsync(AuthConfig => AuthConfig.AuthConfigId == id, AuthConfig);
        }

        public async Task DeleteAsync(ObjectId id)
        {
            await _AuthConfigurations.DeleteOneAsync(AuthConfig => AuthConfig.AuthConfigId == id);
        }
    }
}
