using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Scaffolding.Theme
{
    public class ConfigThemeDto
    {
        [MaxLength(200)]
        public string? ColorButtonsAction { get; set; }
        [MaxLength(200)]
        public string? ColorButtonCancel { get; set; }
        [MaxLength(200)]
        public string? ColorButtonCreate { get; set; }
        [MaxLength(200)]
        public string? ColorButtonText { get; set; }
        [MaxLength(200)]
        public string? ColorTitle { get; set; }
        [MaxLength(200)]
        public string? ColorSubtitle { get; set; }
        [MaxLength(200)]
        public string? ColorText { get; set; }
        [MaxLength(200)]
        public string? ColorTextMenu { get; set; }
        [MaxLength(200)]
        public string? ColorBackgroundMenu { get; set; }
        [MaxLength(200)]
        public string? ColorHeaderTable { get; set; }
        [MaxLength(200)]
        public string? ColorTextColumn { get; set; }
        [MaxLength(200)]
        public string? TypeTitle { get; set; }
        [MaxLength(200)]
        public string? TypeSubtitle { get; set; }
        [MaxLength(200)]
        public string? TypeParagraph { get; set; }
        [MaxLength(200)]
        public string? StyleLetter { get; set; }
        [MaxLength(200)]
        public string? CompanyName { get; set; }        
    }
}
