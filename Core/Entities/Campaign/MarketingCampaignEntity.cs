using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Campaign
{
    [Table("MarketingCampaigns", Schema = "Campaign")]
    public class MarketingCampaignEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(200)]
        public string OriginId { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(10)]
        public string HourForSending { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime SyncedAt { get; set; }


        public DateTime StarDateRecurring { get; set; }

        public DateTime EndDateRecurring { get; set; }

        public bool IndefiniteEndDate { get; set; }

        [ForeignKey("MarketingEntity")]
        public int MarketingId { get; set; }
        public MarketingEntity MarketingEntity { get; set; } = null!;

        [ForeignKey("RecurringTypeEntity")]
        public int RecurringTypeId { get; set; }
        public RecurringTypeEntity RecurringTypeEntity { get; set; } = null!;

        public ICollection<MarketingUserEntity> MarketingUserEntities { get; set; }
        public ICollection<MarketingTemplateEntity> MarketingTemplateEntities { get; set; }
    }
}
