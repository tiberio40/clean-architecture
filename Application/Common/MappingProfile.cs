using AutoMapper;
using Core.DTOs.Campaign.Marketing;
using Core.DTOs.OAuth;
using Core.Entities.Campaign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.Campaign.Marketing.MetasCredentialsDto;

namespace Application.Common
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateMarketingDto, MarketingEntity>();
            CreateMap<MarketingEntity, CreateMarketingDto>();
            CreateMap<OAuth1Dto, OAuth1AuthorizationDto>();
            CreateMap<MetasCredentialsDto, OAuthWhatsAppDto>();
            CreateMap<OAuthWhatsAppDto, OAuthWhatsAppResponseDto>();
        }
    }
}
