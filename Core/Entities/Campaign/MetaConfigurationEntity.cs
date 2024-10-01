using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Campaign
{
    [Table("MetaConfigurations", Schema = "Campaign")]
    public class MetaConfigurationEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(250)]
        public string Name { get; set; } = String.Empty;

        [MaxLength(250)]
        public string OAuthId { get; set; } = String.Empty;

        [ForeignKey("MetaTypeServiceEntity")]
        public int MetaTypeServiceId { get; set; }
        public MetaTypeServiceEntity MetaTypeServiceEntity { get; set; } = null!;

        public ICollection<MarketingEntity> MarketingEntities { get; set; }


    }
}
