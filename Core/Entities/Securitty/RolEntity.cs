using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Securitty
{
    [Table("Rol", Schema = "Security")]
    public class RolEntity
    {
        public RolEntity()
        {
            UserEntities = new HashSet<UserEntity>();
            RolesPermissionsEntities = new HashSet<RolesPermissionsEntity>();
            RolMenusEntities = new HashSet<RolMenusEntity>();
        }
        [Key]
        public int IdRol { get; set; }

        [MaxLength(100)]
        public string Rol { get; set; } = null!;

        public IEnumerable<UserEntity> UserEntities { get; set; }
        public IEnumerable<RolesPermissionsEntity> RolesPermissionsEntities { get; set; }
        public IEnumerable<RolMenusEntity> RolMenusEntities { get; set; }
    }
}
