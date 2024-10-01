using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Securitty
{
    public class RolMenusEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("RolEntity")]
        public int IdRol { get; set; }
        public RolEntity RolEntity { get; set; } = null!;


        [ForeignKey("MenuEntity")]
        public int IdMenu { get; set; }
        public MenuEntity MenuEntity { get; set; } = null!;
    }
}
