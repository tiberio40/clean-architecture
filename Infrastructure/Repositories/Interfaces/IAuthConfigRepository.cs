using Core.Entities;
using Core.Entities.Oauth;
using MongoDB.Bson;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IAuthConfigRepository
    {
        Task<List<AuthConfigEntity>> GetAllAsync();
        Task<AuthConfigEntity> GetByIdAsync(ObjectId id);
        Task<AuthConfigEntity> GetByConnectionNameAsync(string connectionName);
        Task<List<AuthConfigEntity>> FindAll(Expression<Func<AuthConfigEntity, bool>> where);
        Task CreateAsync(AuthConfigEntity AuthConfig);
        Task UpdateAsync(ObjectId id, AuthConfigEntity AuthConfig);
        Task DeleteAsync(ObjectId id);
    }
}