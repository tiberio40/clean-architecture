using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Campaign
{
    [Table("MarketingTemplates", Schema = "Campaign")]
    public class MarketingTemplateEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = string.Empty;

        [MaxLength]
        public string Message { get; set; } = string.Empty;

        [MaxLength]
        public string FormValues { get; set; } = string.Empty;

        [MaxLength(250)]
        public string TemplateMetaId { get; set; } = string.Empty;

        [MaxLength(250)]
        public string TitleTemplateMeta { get; set; } = string.Empty;

        [ForeignKey("MarketingCampaignEntity")]
        public int MarketingCampaignId { get; set; }
        public MarketingCampaignEntity MarketingCampaignEntity { get; set; } = null!;
    }
}
