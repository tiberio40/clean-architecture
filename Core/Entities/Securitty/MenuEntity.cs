using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Securitty
{
    [Table("Menu", Schema = "Security")]
    public class MenuEntity
    {
        public MenuEntity()
        {
            RolMenusEntities = new HashSet<RolMenusEntity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [MaxLength(200)]
        [Required]
        public string Url { get; set; } = null!;

        [MaxLength(100)]
        public string? Icon { get; set; }

        [MaxLength(200)]
        [Required]
        public string Name { get; set; } = null!;

        public bool IsPrincipal { get; set; }

        public int? IdMenuPrimary { get; set; }

        public IEnumerable<RolMenusEntity> RolMenusEntities { get; set; }
    }
}
