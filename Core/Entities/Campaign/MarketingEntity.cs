using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Campaign
{
    [Table("Marketings", Schema = "Campaign")]
    public class MarketingEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(250)]
        public string Name { get; set; } = String.Empty;

        [MaxLength(50)]
        public string Cover { get; set; } = String.Empty;

        [MaxLength(250)]
        public string OAuthId { get; set; } = String.Empty;

        [ForeignKey("MarketingStatusEntity")]
        public int MarketingStatusId { get; set; }
        public MarketingStatusEntity MarketingStatusEntity { get; set; } = null!;

        [ForeignKey("MetaConfigurationEntity")]
        public int? MetaConfigurationId { get; set; }
        public MetaConfigurationEntity MetaConfigurationEntity { get; set; } = null!;
    }
}
