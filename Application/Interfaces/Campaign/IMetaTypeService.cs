using Core.DTOs.Campaign.Marketing;
using Core.Entities.Campaign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.Campaign.Marketing.MetasCredentialsDto;

namespace Application.Interfaces.Campaign
{
    public interface IMetaTypeService
    {
        public IEnumerable<MetaTypeServiceEntity>  GetAll();

        public Task<bool> SetCredentials(MetasCredentialsDto model);

        public Task<OAuthWhatsAppResponseDto> GetCredentialsById(int id);

        public IEnumerable<CredentialsResponseDto> GetCredentials();
    }
}
