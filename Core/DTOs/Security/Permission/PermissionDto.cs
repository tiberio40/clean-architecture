namespace Core.DTOs.Security.Permission
{
    public class PermissionDto
    {
        public int IdPermission { get; set; }
        public string Permission { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Ambit { get; set; } = null!;
    }
}
