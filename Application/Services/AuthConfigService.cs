using Common.Utility;
using Core.DTOs.OAuth;
using Core.Entities;
using Core.Entities.Oauth;
using Core.Interfaces;
using Infrastructure.Repositories.Interfaces;
using MongoDB.Bson;
using System.Linq.Expressions;

namespace Application.Services
{
    public class AuthConfigService : IAuthConfigService
    {
        private readonly IAuthConfigRepository _AuthConfigRepository;

        public AuthConfigService(IAuthConfigRepository AuthConfigRepository)
        {
            _AuthConfigRepository = AuthConfigRepository;
        }

        public async Task<List<AuthConfigEntity>> GetAllAuthConfigsAsync()
        {
            return await _AuthConfigRepository.GetAllAsync();
        }

        public async Task<List<AuthConfigEntity>> FindAllAsync(Expression<Func<AuthConfigEntity, bool>> where)
        {
            return await _AuthConfigRepository.FindAll(where);
        }

        public async Task<AuthConfigEntity> GetAuthConfigByIdAsync(ObjectId  id)
        {
            return await _AuthConfigRepository.GetByIdAsync(id);
        }

        public async Task<AuthConfigEntity> GetAuthConfigByConnectionNameAsync(string connectionName)
        {
            return await _AuthConfigRepository.GetByConnectionNameAsync(connectionName);
        }

        public async Task<AuthConfigEntity> CreateAuthConfigAsync<T>(OAuthDto<T> model)
        {
            AuthConfigEntity authConfig = new AuthConfigEntity()
            {
                AuthConfigId = model.AuthConfigId,
                ConnectionName = model.ConnectionName,
                Version = model.Version,
                DataOrigin = model.DataOrigin,
                Auth = model.Auth.AsDictionary()
            };

            authConfig.Version = model.Auth switch
            {
                OAuthBearerDto _ => "BEARER",
                OAuth1Dto _ => "OAUTH1.0",
            };

            await _AuthConfigRepository.CreateAsync(authConfig);

            return authConfig;
        }

        public async Task CreateAuthConfigAsync(AuthConfigEntity AuthConfig)
        {
            await _AuthConfigRepository.CreateAsync(AuthConfig);
        }

        public async Task UpdateAuthConfigAsync(ObjectId  id, AuthConfigEntity AuthConfig)
        {
            await _AuthConfigRepository.UpdateAsync(id, AuthConfig);
        }


        public async Task DeleteAuthConfigAsync(ObjectId id)
        {
            await _AuthConfigRepository.DeleteAsync(id);
        }
    }
}
