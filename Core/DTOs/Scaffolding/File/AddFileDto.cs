using Microsoft.AspNetCore.Http;
using static Common.Enums.Enums;

namespace Core.DTOs.Scaffolding.File
{
    public class AddFileDto
    {
        public IFormFile File { get; set; } = null!;
        public TypeFile TypeFile { get; set; }
    }
}
