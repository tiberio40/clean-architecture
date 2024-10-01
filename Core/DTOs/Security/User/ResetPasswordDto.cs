using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Security.User
{
    public class ResetPasswordDto : PasswordConfirmationDto
    {
        [Required(ErrorMessage = "El id de usuario es requerido")]
        public int IdUser { get; set; }
    }
}
