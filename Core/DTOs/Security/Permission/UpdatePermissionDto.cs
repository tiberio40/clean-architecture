using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Security.Permission
{
    public class UpdatePermissionDto
    {
        public UpdatePermissionDto()
        {
            IdPermissions = new List<int>();
        }
        [Required(ErrorMessage = "El campo [IdRol] es obligatorio.")]
        public int IdRol { get; set; }
        public List<int> IdPermissions { get; set; }
    }
}
