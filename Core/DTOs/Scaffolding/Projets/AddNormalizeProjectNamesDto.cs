using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Scaffolding.Projets
{
    public class AddNormalizeProjectNamesDto
    {
        [Required(ErrorMessage = "Nombre del proyecto TMetric es requerido.")]
        public string NameTMetric { get; set; } = null!;
        [Required(ErrorMessage = "Nombre del proyecto NetSuite es requerido.")]
        public string NameNetSuite { get; set; } = null!;
    }
}
