using Application.Interfaces;
using Common.Constant;
using Core.DTOs.OAuth;
using Core.DTOs.TmetricDtos;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Options;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tmetric
{
    public class TmetricRequestService : ITmetricRequestService
    {
        BearerAuthorizationDto token;

        private readonly IExternalRequestClient _externalRequestClient;
        private readonly IMembersRepository _membersRepository;
        private readonly ITimeEntryRepository _timeEntryRepository;

        public IConfiguration Configuration { get; }

        public TmetricRequestService(IExternalRequestClient externalRequestClient, ITimeEntryRepository timeEntryRepository, IMembersRepository membersRepository, IConfiguration configuration)
        {
            Configuration = configuration;
            _externalRequestClient = externalRequestClient;
            _timeEntryRepository = timeEntryRepository;
            _membersRepository = membersRepository;

            var authTimectricSetting = Configuration.GetSection("AuthTimetric");

            token = new BearerAuthorizationDto()
            {
                BaseUrl = authTimectricSetting.GetSection("BaseUrl").Value,
                TokenSecret = authTimectricSetting.GetSection("TokenSecret").Value,
            };

        }
        public async Task<UserProfileDto?> GetUserAdmin()
        {
            UserProfileDto userProfileDto = await _externalRequestClient.Get<UserProfileDto, BearerAuthorizationDto>(token.BaseUrl, "userprofile", token);
          
            return userProfileDto;
        }

        public async Task<List<UserProfileTimeDto>?> GetAllTimeEntry(string account, string startTime, string endTime, bool useUtcTime = true)
        {
            var endpoint = $"accounts/{account}/timeentries/group/?StartTime={startTime}&EndTime={endTime}&useUtcTime={useUtcTime}";

            var timeEntries = await _externalRequestClient.Get<List<UserProfileTimeDto>, BearerAuthorizationDto>(token.BaseUrl, endpoint, token);

            SimplifiedTimeEntryDto simplifiedTimeEntryDto = new SimplifiedTimeEntryDto();

            timeEntries?.ForEach(async time => {

                var email = await _membersRepository.GetEmailByUserProfile(time.userProfileId);


                time.entries.ForEach(entry => {
                    simplifiedTimeEntryDto.email = email;
                    simplifiedTimeEntryDto.durationHours = entry.durationHours;
                    simplifiedTimeEntryDto.startTime = entry.startTime;
                    simplifiedTimeEntryDto.endTime = entry.endTime;
                    simplifiedTimeEntryDto.projectName = entry.projectName;
                    simplifiedTimeEntryDto.description = entry.details.description;
                    simplifiedTimeEntryDto.projectId = entry.details.projectId;
                    simplifiedTimeEntryDto.userProfileId = time.userProfileId;
                    simplifiedTimeEntryDto.userName = time.userName;
                    simplifiedTimeEntryDto.timeZone = time.timeZone;
                    _timeEntryRepository.AddTimeEntry(simplifiedTimeEntryDto);
                    });

                });

                return timeEntries;
            }

        public async Task<List<TimeEntryDto>?> GetTimeEntriesByUser(int account, string email, string startTime, string endTime, bool useUtcTime, bool includeDeleted, bool truncate)
        {
            var userId = await _membersRepository.GetUserProfileIdByEmail(email);
            if (userId != null)
            {
                var endpoint = $"accounts/{account}/timeentries/{userId}?StartTime={startTime}&EndTime={endTime}&useUtcTime={useUtcTime}&includeDeleted={includeDeleted}&truncate={truncate}";
                var timeEntries = await _externalRequestClient.Get<List<TimeEntryDto>, BearerAuthorizationDto>(token.BaseUrl, endpoint, token);
                return timeEntries;

            }
            return null;
        }
    }

}
