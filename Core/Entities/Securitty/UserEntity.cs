using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Securitty
{
    [Table("User", Schema = "Security")]
    public class UserEntity
    {

        [Key]
        public int IdUser { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        public bool VerefiedEmail { get; set; }

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public DateTime RegisterDate { get; set; }

        public DateTime? UpdateDate { get; set; }
        public bool PasswordReset { get; set; } = false;

        public bool IsActive { get; set; } = true;

        [ForeignKey("RolEntity")]
        public int IdRol { get; set; }

        public RolEntity RolEntity { get; set; } = null!;

        [NotMapped]
        public string FullName { get { return $"{this.Name} {this.LastName}"; } }

    }
}
