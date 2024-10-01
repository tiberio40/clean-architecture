using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Security.User
{
    public class AddUserDto : LoginDto
    {
        [Required(ErrorMessage = "El nombre del usuario es requerido.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "El apellido del usuario es requerido.")]
        public string LastName { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Required(ErrorMessage = "La confirmación de la contraseña es requerida.")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
