using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Campaign
{
    [Table("MarketingUsers", Schema = "Campaign")]
    public class MarketingUserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(200)]
        public string ContactName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string ContactPhone { get; set; } = string.Empty;

        [MaxLength(200)]
        public string ContactId { get; set; } = string.Empty;

        [ForeignKey("MarketingCampaignEntity")]
        public int MarketingCampaignId { get; set; }
        public MarketingCampaignEntity MarketingCampaignEntity { get; set; } = null!;

    }
}
