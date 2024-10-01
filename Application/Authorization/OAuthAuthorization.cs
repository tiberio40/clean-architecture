using Core.DTOs.OAuth;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization
{
    public class OAuthAuthorization: IExternalAuthorizationService
    {
        private readonly OAuthAuthorizationDto _oAuthDto;
        public OAuthAuthorization(OAuthAuthorizationDto oAuthDto) {
            _oAuthDto = oAuthDto;
        }
        public AuthenticationHeaderValue Authorize()
        {
            return new AuthenticationHeaderValue("OAuth", _oAuthDto.TokenSecret);
        }
    }
}
