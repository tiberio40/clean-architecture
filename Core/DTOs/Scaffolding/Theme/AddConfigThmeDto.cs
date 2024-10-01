using Microsoft.AspNetCore.Http;

namespace Core.DTOs.Scaffolding.Theme
{
    public class AddConfigThmeDto : ConfigThemeDto
    {
        public IFormFile? ImgLogo { get; set; }
    }
}
