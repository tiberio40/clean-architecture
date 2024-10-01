using Core.DTOs.Security.Menus;

namespace Application.Interfaces.Security
{
    public interface IMenuServices
    {
        List<MenuDto> GetAllMenuByRol(int idRol);

        Task<bool> UpdateMenuRol(RolMenuDto update);
    }
}
