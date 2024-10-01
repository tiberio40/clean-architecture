using Application.Interfaces;
using Core.DTOs.OAuth;
using Core.DTOs.TmetricDtos;
using Core.Interfaces;
using Infrastructure.Common;
using Infrastructure.Options;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tmetric
{
    public class MembersRequestService : IMembersRequestService
    {
        BearerAuthorizationDto token;
        private readonly IExternalRequestClient _externalRequestClient;
        private readonly IMembersRepository _membersRepository;
        public IConfiguration Configuration { get; }

        public MembersRequestService(IExternalRequestClient externalRequestClient, IMembersRepository membersRepository, IConfiguration configuration)
        {
            _externalRequestClient = externalRequestClient;
            _membersRepository = membersRepository;

            Configuration = configuration;
            var authTimectricSetting = Configuration.GetSection("AuthTimetric");

            token = new BearerAuthorizationDto()
            {
                BaseUrl = authTimectricSetting.GetSection("BaseUrl").Value,
                TokenSecret = authTimectricSetting.GetSection("TokenSecret").Value,
            };  

        }
        public async Task<MembersDto> GetMemberById(string id)
        {
            MembersDto membersDto = await _externalRequestClient.Get<MembersDto, BearerAuthorizationDto>(token.BaseUrl, "members", token);
            return membersDto;
        }

        public async Task<IEnumerable<MembersDto>> GetAllMembers(string account)
        {
            var endpoint = $"accounts/{account}/members";
            var membersDto = await _externalRequestClient.Get<List<MembersDto>, BearerAuthorizationDto>(token.BaseUrl, endpoint, token);

            membersDto?.ForEach(time => {
                SimplifiedMembersDto simplifiedMembersDto = new SimplifiedMembersDto();
                simplifiedMembersDto.AccountMemberId = time.AccountMemberId;
                simplifiedMembersDto.UserProfileId = time.UserProfile.UserProfileId;
                simplifiedMembersDto.RoleKey = time.RoleKey;
                simplifiedMembersDto.AccountId = time.AccountId;
                simplifiedMembersDto.UserName = time.UserProfile.UserName;
                simplifiedMembersDto.Email = time.UserProfile.Email;

                _membersRepository.AddMembers(simplifiedMembersDto);

            });
            return membersDto;
        }
    }
}
