using Application.Interfaces.Campaign;
using AutoMapper;
using Common.Utility;
using Core.DTOs.Campaign.Marketing;
using Core.DTOs.OAuth;
using Core.Entities.Campaign;
using Core.Entities.Oauth;
using Core.Interfaces;
using Infrastructure.UnitOfWork.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Constant.Const;
using static Core.DTOs.Campaign.Marketing.MetasCredentialsDto;

namespace Application.Services.Campaign
{
    public class MetaTypeService: IMetaTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthConfigService _AuthConfigService;

        public MetaTypeService(IUnitOfWork unitOfWork, IMapper mapper, IAuthConfigService authConfigService) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _AuthConfigService = authConfigService;
        }

        public IEnumerable<MetaTypeServiceEntity> GetAll()
        {
            return _unitOfWork.MetaTypeServiceRepository.FindAll(x => x.IsEnabled == true).OrderBy(x => x.Name);
        }

        public IEnumerable<CredentialsResponseDto> GetCredentials() {
            return _unitOfWork.MetaConfigurationRepository.GetAll(x => x.MetaTypeServiceEntity)
                .Select(x => new CredentialsResponseDto() { 
                    Id = x.Id,
                    MetaType = x.MetaTypeServiceEntity.Name,
                    Code = x.MetaTypeServiceEntity.Code
                });
        }

        public async Task<OAuthWhatsAppResponseDto> GetCredentialsById(int id)
        {
            OAuthWhatsAppResponseDto result;
            var metaConfiguration = _unitOfWork.MetaConfigurationRepository
                .GetAll(x => x.MetaTypeServiceEntity)
                .FirstOrDefault(o => o.Id == id && o.OAuthId != string.Empty) ?? new MetaConfigurationEntity();

            if (metaConfiguration.Id != 0)
            {
                var oauth = await _AuthConfigService.GetAuthConfigByIdAsync(ObjectId.Parse(metaConfiguration.OAuthId));
                result = _mapper.Map<OAuthWhatsAppResponseDto>(oauth.Auth.ToObject<OAuthWhatsAppDto>());
                result.MetaType = metaConfiguration.MetaTypeServiceEntity.Code;
                return result;
            }

            return new OAuthWhatsAppResponseDto();
        }

        public async Task<bool> SetCredentials(MetasCredentialsDto model) {

            try
            {
                MetaConfigurationEntity metaConfiguration;

                var metaType = _unitOfWork.MetaTypeServiceRepository.GetAll(x => x.MetaConfigurationEntities).FirstOrDefault(x => x.Code == model.MetaType.ToLower());


                metaConfiguration = metaType.MetaConfigurationEntities.FirstOrDefault() ?? new MetaConfigurationEntity()
                {
                    MetaTypeServiceId = metaType.Id,
                    Name = model.Name,
                };
                
                
                OAuthWhatsAppDto oauthModel = _mapper.Map<OAuthWhatsAppDto>(model);

                AuthConfigEntity oauth = new AuthConfigEntity()
                {
                    DataOrigin = ClientConst.Meta,
                    ConnectionName = ClientConst.NetSuite,
                    Version = OAuthTypeConst.Bearer,
                    Auth = oauthModel.AsDictionary()
                };

                if (metaConfiguration.OAuthId == string.Empty)
                {
                    await _AuthConfigService.CreateAuthConfigAsync(oauth);
                    metaConfiguration.OAuthId = oauth.AuthConfigId.ToString();
                    _unitOfWork.MetaConfigurationRepository.Insert(metaConfiguration);
                }
                else {
                    oauth.AuthConfigId = ObjectId.Parse(metaConfiguration.OAuthId);
                    await _AuthConfigService.UpdateAuthConfigAsync(oauth.AuthConfigId, oauth);

                    _unitOfWork.MetaConfigurationRepository.Update(metaConfiguration);
                }

                return await _unitOfWork.Save() == 1 ? true: false;

            }
            catch (Exception ex) { 
            
                string message = ex.Message;
                throw new Exception(message, ex);
            }
        }

        
    }
}
