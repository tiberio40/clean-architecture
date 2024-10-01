using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Securitty
{
    [Table("RolesPermissions", Schema = "Security")]
    public class RolesPermissionsEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("RolEntity")]
        public int IdRol { get; set; }
        public RolEntity RolEntity { get; set; } = null!;


        [ForeignKey("PermissionEntity")]
        public int IdPermission { get; set; }
        public PermissionEntity PermissionEntity { get; set; } = null!;
    }
}
