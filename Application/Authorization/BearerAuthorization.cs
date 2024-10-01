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
    public class BearerAuthorization : IExternalAuthorizationService
    {
        private readonly BearerAuthorizationDto _oAuthBearerDto;


        public BearerAuthorization(BearerAuthorizationDto oAuthBearerDto)
        {
            _oAuthBearerDto = oAuthBearerDto;
        }

        public AuthenticationHeaderValue Authorize()
        {
            return new AuthenticationHeaderValue("Bearer", _oAuthBearerDto.TokenSecret);
        }
    }
}
