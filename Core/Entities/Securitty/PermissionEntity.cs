using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Securitty
{
    [Table("Permission", Schema = "Security")]
    public class PermissionEntity
    {
        public PermissionEntity()
        {
            RolesPermissionsEntities = new HashSet<RolesPermissionsEntity>();
        }
        [Key]
        public int IdPermission { get; set; }
        [MaxLength(100)]
        public string Permission { get; set; } = null!;
        [MaxLength(300)]
        public string Description { get; set; } = null!;

        [MaxLength(100)]
        public string Ambit { get; set; } = null!;

        public IEnumerable<RolesPermissionsEntity> RolesPermissionsEntities { get; set; }
    }
}
