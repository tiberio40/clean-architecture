using Core.DTOs;
using Core.DTOs.Campaign.Marketing;
using Core.DTOs.OAuth;
using Core.Entities.Campaign;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Campaign
{
    public interface IMarketingService
    {
        public IEnumerable<MarketingCardDto> GetAllMarketings();
        public MarketingCardDto GetMarketingById(int id);
        public IEnumerable<MarketingUserEntity> GetMarketingUsers(MarketingUserDto model);
        public IEnumerable<MarketingUserEntity> GetMarketingUsers(int marketingCampaignId);
        public MarketingCampaignEntity GetMarketingCampaign(int id);
        public IEnumerable<MarketingCampaignEntity> GetMarketingCampaigns(MarketingCampaignDto model);
        public IEnumerable<MarketingCampaignEntity> GetMarketingCampaigns(int marketingId);
        public ValuesForTemplateDetailDto GetMarketingTemplate(int marketingCampaignId);
        public Task<MarketingEntity> CreateMarketing(CreateMarketingDto model);
        public Task<MarketingEntity> EditMarketing(int id, CreateMarketingDto model);
        public Task<ResponseDto> LoadMarketingCampaign(int id);
        public Task<bool> SetOAuthSource(int id, OAuth1Dto model);
        public Task<OAuth1Dto> GetOAuthSource(int id);
        public Task UpdateTemplateStatus();
        public Task SendingMessages();
        public Task<UploadFileMetaResponseDto> UploadFileForTemplate(IFormFile file, int marketingCampaignId);
        public Task EditTemplate(CreateTemplateDto model);
        public Task<int> SetRecurring(RecurringDto model);

        public Task<bool> SetMetaCredentials(int id, MetaCretentialDto model);
    }
}
