using Core.DTOs.OAuth;
using Core.Entities;
using Core.Entities.Oauth;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface IAuthConfigService
    {
        Task<List<AuthConfigEntity>> GetAllAuthConfigsAsync();
        Task<AuthConfigEntity> GetAuthConfigByIdAsync(ObjectId id);
        Task<List<AuthConfigEntity>> FindAllAsync(Expression<Func<AuthConfigEntity, bool>> where);
        Task<AuthConfigEntity> GetAuthConfigByConnectionNameAsync(string ConnectionName);
        Task<AuthConfigEntity> CreateAuthConfigAsync<T>(OAuthDto<T> AuthConfig);
        Task CreateAuthConfigAsync(AuthConfigEntity AuthConfig);
        Task UpdateAuthConfigAsync(ObjectId  id, AuthConfigEntity AuthConfig);
        Task DeleteAuthConfigAsync(ObjectId  id);
    }
}