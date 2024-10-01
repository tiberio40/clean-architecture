using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Campaign
{
    [Table("MarketingStatus", Schema = "Campaign")]
    public class MarketingStatusEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(250)]
        public string Name { get; set; } = String.Empty;

        [MaxLength(50)]
        public string Code { get; set; } = String.Empty;
    }
}
