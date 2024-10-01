using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Campaign.Marketing
{
    public class CreateMarketingDto
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; } = String.Empty;

        [Required]
        [MaxLength(50)]
        public string Cover { get; set; } = String.Empty;
    }

    public class MarketingCardDto {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cover { get; set; }
        public string Status { get; set; }
        public string CodeStatus { get; set; }
        public bool HasCredentials { get; set; }
    }

    public class MarketingUserDto
    {
        [Required]
        public int? MarketingCampaignId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int? Start { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int? End { get; set; }
    }

    public class MarketingCampaignDto
    {
        [Required]
        public int? MarketingId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int? Start { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int? End { get; set; }
    }

    public class MarketingTemplateDto  {

        public int Id { get; set; }

        public string Status { get; set; } 

        public WSATemplateDto Message { get; set; }

        public string TemplateMetaId { get; set; }

        public int MarketingCampaignId { get; set; }
    }

    public class RecurringDto {
        [Required]
        public int CampaignId { get; set; }

        [Required]
        [RegularExpression(@"^(0|[1-9]|1[0-9]|2[0-3]):(0|[1-9]|[1-5][0-9])$", ErrorMessage = "The hour format must be hh:mm for example 0:0, 14:0 or 20:59")]
        public string Hour { get; set; }
    }

    public class MetaCretentialDto {

        [Required]
        [Range(1, int.MaxValue)]
        public int MetaConfigurationId { get; set; }
    }
}
