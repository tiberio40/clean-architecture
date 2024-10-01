namespace Core.DTOs.Security.Permission
{
    public class PermissionRolDto : PermissionDto
    {
        public bool IsAssigned { get; set; }
        public int IdRol { get; set; }
    }
}
