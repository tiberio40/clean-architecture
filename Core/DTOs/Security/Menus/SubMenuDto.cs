namespace Core.DTOs.Security.Menus
{
    public class SubMenuDto
    {
        public int IdSubMenu { get; set; }

        public string Name { get; set; } = null!;

        public string Url { get; set; } = null!;

        public string? Icon { get; set; }

        public bool IsAssigned { get; set; }
    }
}
