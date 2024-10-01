using Core.DTOs.Security.Rol;

namespace Application.Interfaces.Security
{
    public interface IRolServices
    {
        List<RolDto> GetAll();

        Task<bool> Update(RolDto update);
    }
}
