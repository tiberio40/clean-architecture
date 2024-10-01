using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Scaffolding
{
    [Table("NormalizeProjectNames", Schema = "Scaffolding")]
    public class NormalizeProjectNamesEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NameTMetric { get; set; } = null!;
        [Required]
        public string NameNetSuite { get; set; } = null!;
    }
}
