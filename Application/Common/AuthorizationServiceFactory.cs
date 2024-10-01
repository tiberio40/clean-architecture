using Application.Authorization;
using Common.Exceptions;
using Core.DTOs.OAuth;
using Core.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Common
{
    public class AuthorizationServiceFactory
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public AuthorizationServiceFactory(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public IExternalAuthorizationService Create<T>(T oauthType)
        {           
            IExternalAuthorizationService factory = oauthType switch
            {
                BearerAuthorizationDto _ => new BearerAuthorization(oauthType as BearerAuthorizationDto ?? new BearerAuthorizationDto()),
                OAuth1AuthorizationDto _ => new OAuth1Authorization(oauthType as OAuth1AuthorizationDto ?? new OAuth1AuthorizationDto()),
                OAuthAuthorizationDto _ => new OAuthAuthorization(oauthType as OAuthAuthorizationDto ?? new OAuthAuthorizationDto()),
                _ => throw new ExternalAuthorizationException($"ClientName  doesn't support")
            };
            return factory;
        }
    }
}