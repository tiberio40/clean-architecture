namespace Core.DTOs.Security.Menus
{
    public class RolMenuDto
    {
        public RolMenuDto()
        {
            IdMenues = new List<MenuSubMenuDto>();
        }

        public int IdRol { get; set; }
        public List<MenuSubMenuDto> IdMenues { get; set; }
    }
}
