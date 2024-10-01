using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Security.User
{
    public class PasswordConfirmationDto
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La contraseña es requerida")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
