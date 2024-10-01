using Common.Enums;
using Core.DTOs.Security.Permission;

namespace Application.Interfaces.Security
{
    public interface IPermissionServices
    {
        List<PermissionDto> GetAllPermission();
        List<PermissionRolDto> GetAllPermissionsByRol(int idRol);
        bool ValidatePermissionByUser(Enums.Permission permission, int idUser, int idRol);
        Task<bool> UpdatePermission(UpdatePermissionDto update);
    }
}
