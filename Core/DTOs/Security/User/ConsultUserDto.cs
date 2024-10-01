namespace Core.DTOs.Security.User
{
    public class ConsultUserDto : LoginDto
    {
        public int IdUser { get; set; }
        public bool VerefiedEmail { get; set; }
        public DateTime RegisterDate { get; set; }
        public int IdRol { get; set; }
        public string Rol { get; set; }
        public string FullName { get; set; }
        public bool IsActive{ get; set; }
    }
}
