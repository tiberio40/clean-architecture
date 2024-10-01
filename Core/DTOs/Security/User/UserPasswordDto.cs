using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Security.User
{
    public class UserPasswordDto : PasswordConfirmationDto
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La antigua contraseña es requerida")]
        [Display(Name = "Antigua contraseña")]
        public string OldPassword { get; set; } = null!;
    }
}
