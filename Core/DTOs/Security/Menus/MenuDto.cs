namespace Core.DTOs.Security.Menus
{
    public class MenuDto
    {
        public MenuDto()
        {
            SubMenus = new List<SubMenuDto>();
        }

        public string Name { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string? Icon { get; set; }
        public bool IsPrincipal { get; set; }
        public bool IsAssigned { get; set; }
        public int IdMenu { get; set; }
        public List<SubMenuDto> SubMenus { get; set; }
    }
}
